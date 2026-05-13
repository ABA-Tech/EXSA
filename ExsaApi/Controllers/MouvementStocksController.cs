using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExsaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MouvementStocksController : ControllerBase
    {
        private readonly IMouvementStockService _mouvementStockService;
        private readonly IArticleStockService _articleService;
        private readonly IAppService<Utilisateur> _utilisateurService;

        public MouvementStocksController(IMouvementStockService mouvementStockService,
             IAppService<Utilisateur> utilisateurService,
             IArticleStockService articleService)
        {
            _mouvementStockService = mouvementStockService ?? throw new ArgumentNullException(nameof(mouvementStockService));
            _utilisateurService = utilisateurService ?? throw new ApplicationException(nameof(utilisateurService));
            _articleService = articleService;

        }

        [HttpGet]
        public async Task<IActionResult> Get(string idArticle=null)
        {
            return Ok(await _mouvementStockService.GetAllAsync(idArticle));
        }

        // GET api/<MouvementStocksController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _mouvementStockService.GetByIdAsync(id));
        }

        // POST api/<MouvementStocksController>
        [HttpPost]
        public async Task<IActionResult> Post(MouvementStock mouvementStock)
        {

            var result = await _utilisateurService.GetAllAsync();

            if (result == null || !result.Any())
                throw new Exception("Erreur de création de l'utilisateur");
            
            mouvementStock.IdOperateur = result.First().IdUtilisateur;

            var mvt = await _mouvementStockService.CreateAsync(mouvementStock);

            return Ok(mvt);
        }

        // PUT api/<MouvementStocksController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, MouvementStock mouvementStock)
        {
            if (id != mouvementStock.IdMouvement)
            {
                return BadRequest();
            }

            return Ok(await _mouvementStockService.UpdateAsync(mouvementStock));
        }

        // DELETE api/<MouvementStocksController>/5
        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _mouvementStockService.DeleteByIdAsync(id);
        }
    }
}
