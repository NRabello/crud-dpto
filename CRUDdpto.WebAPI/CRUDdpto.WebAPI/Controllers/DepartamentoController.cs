using CRUDdpto.WebAPI.Models;
using CRUDdpto.WebAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace CRUDdpto.WebAPI.Controllers
{
    [ApiController]
    [Route("departamento")]

    public class DepartamentoController : ControllerBase
    {

        private readonly DepartamentoDAO dptoDAO;

        public DepartamentoController()
        {
            dptoDAO = new DepartamentoDAO();
        }


        [HttpPost]
        [Route("create")]
        public IActionResult CreateDpto([FromBody] Departamento departamento)
        {
            try
            {
                dptoDAO.Create(departamento);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar departamento: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("read")]
        public IActionResult ReadDptos()
        {
            try
            {
                return Ok(dptoDAO.Read());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao retornar departamentos: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("update")]
        public IActionResult updateDpto([FromBody] Departamento departamento)
        {
            try
            {
                if(dptoDAO.FindById(departamento.GetId()) == null) {
                    return StatusCode(500, $"Departamento não encontrado!");
                }
                dptoDAO.Update(departamento);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar o departamento: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult deleteDpto(int id)
        {
            try
            {
                if (dptoDAO.FindById(id) == null)
                {
                    return StatusCode(500, $"Departamento não encontrado!");
                }
                dptoDAO.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao deletar o departamento: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("findById")]
        public IActionResult findDptoById(int id)
        {
            try
            {
                if (dptoDAO.FindById(id) == null) {
                    return StatusCode(500, $"Departamento não encontrado!");
                }
                return Ok(dptoDAO.FindById(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao retornar departamento: {ex.Message}");
            }
        }
    }
}
