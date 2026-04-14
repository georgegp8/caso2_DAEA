using Caso2_EmpresaServicioConsultoria.Models;
using Caso2_EmpresaServicioConsultoria.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Caso2_EmpresaServicioConsultoria.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public ClientesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cliente>>> GetAll()
    {
        var clientes = await _unitOfWork.GetRepository<Cliente>().GetAll();
        return Ok(clientes);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Cliente>> GetById(Guid id)
    {
        var cliente = await _unitOfWork.GetRepository<Cliente>().GetById(id);
        return cliente is null ? NotFound() : Ok(cliente);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Cliente entity)
    {
        await _unitOfWork.GetRepository<Cliente>().Add(entity);
        await _unitOfWork.Complete();
        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, Cliente entity)
    {
        var cliente = await _unitOfWork.GetRepository<Cliente>().GetById(id);
        if (cliente is null)
            return NotFound();

        entity.Id = id;
        await _unitOfWork.GetRepository<Cliente>().Update(entity);
        await _unitOfWork.Complete();
        return Ok(entity);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var cliente = await _unitOfWork.GetRepository<Cliente>().GetById(id);
        if (cliente is null)
            return NotFound();

        await _unitOfWork.GetRepository<Cliente>().Delete(id);
        await _unitOfWork.Complete();
        return Ok();
    }
}
