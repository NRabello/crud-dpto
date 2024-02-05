using CRUDdpto.WebAPI.Models;
using CRUDdpto.WebAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CRUDdpto.WebAPI.Controllers
{
    [ApiController]
    [Route("funcionario")]
    public class FuncionarioController : ControllerBase
    {

        private readonly FuncionarioDAO funcDAO;
        private readonly DepartamentoDAO dptoDAO;
        public FuncionarioController()
        {
            funcDAO = new FuncionarioDAO();
            dptoDAO = new DepartamentoDAO();
        }

        [HttpPost]
        [Route("create")]
        public IActionResult CreateFunc([FromBody] Funcionario funcionario)
        {
            try
            {   
                Departamento dptoFunc = funcionario.getDepartamento();

                if (dptoDAO.FindById(dptoFunc.GetId()) == null)
                {
                    return StatusCode(500, $"Departamento não encontrado!");
                }
                funcDAO.Create(funcionario);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar funcionario: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("read")]
        public IActionResult ReadFuncs()
        {
            try
            {
                return Ok(funcDAO.Read());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao retornar funcionários: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("update")]
        public IActionResult updateFunc([FromBody] Funcionario funcionario)
        {
            try
            {
                Departamento dptoFunc = funcionario.getDepartamento();

                if (funcDAO.FindById(funcionario.getId()) == null)
                {
                    return StatusCode(500, $"Funcionario não encontrado!");
                }
                else if (dptoDAO.FindById(dptoFunc.GetId()) == null)
                {
                    return StatusCode(500, $"Departamento não encontrado!");
                }
                return Ok(funcDAO.Update(funcionario));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar o funcionario: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult deleteFunc(int id)
        {
            try
            {
                if (funcDAO.FindById(id) == null)
                {
                    return StatusCode(500, $"funcionário não encontrado!");
                }
                funcDAO.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao deletar o funcionário: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("findById")]
        public IActionResult findFuncById(int id)
        {
            try
            {
                if (funcDAO.FindById(id) == null)
                {
                    return StatusCode(500, $"Funcionário não encontrado!");
                }
                return Ok(funcDAO.FindById(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao retornar funcionário: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("findByDepartment")]
        public IActionResult findByDepartment(int id)
        {
            try
            {
                if (dptoDAO.FindById(id) == null)
                {
                    return StatusCode(500, $"Departamento não encontrado!");
                }
                return Ok(funcDAO.FindByDepartment(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao retornar funcionários: {ex.Message}");
            }
        }
    }
}
