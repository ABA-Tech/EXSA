using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExsaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleStockController : ControllerBase
    {
        private readonly IArticleStockService _articleService;
        private readonly IGenericService<Locataire> _locataireService;
        private readonly IMouvementStockService _mouvementStockService;
        private readonly IAppService<Utilisateur> _utilisateurService;

        public ArticleStockController(
            IArticleStockService articleStockService, 
            IGenericService<Locataire> locataireService,
            IMouvementStockService mouvementStockService,
             IAppService<Utilisateur> utilisateurService) 
        {
            _articleService = articleStockService;
            _locataireService = locataireService;
            _mouvementStockService = mouvementStockService;
            _utilisateurService = utilisateurService;
        }

        [HttpGet]
        public async Task<IActionResult> ListerLeStock() 
        { 
            return Ok(await _articleService.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AjouterArticleStock(ArticleStock articleStock) 
        {
            var locataire = (await _locataireService.GetAllAsync()).FirstOrDefault();
            if (locataire == null)
            {
                throw new Exception("Locataire non définit");
            }

            articleStock.DateCreation = DateTime.Now;
            articleStock.IdLocataire = locataire.IdLocataire;

            var result = await _articleService.CreateAsync(articleStock);
            if(result.IdArticle != null)
            {

                var utilisateur = await _utilisateurService.GetAllAsync();

                if (utilisateur == null || !utilisateur.Any())
                    throw new Exception("Erreur de création de l'utilisateur");

                var mvtStock = await _mouvementStockService.CreateAsync(new MouvementStock
                {
                    IdArticle = result.IdArticle,
                    DateMouvement = result.DateCreation.Value,
                    Quantite = result.StockActuel,
                    IdIntervention = null,
                    IdOperateur = utilisateur.First().IdUtilisateur,
                    TypeMouvement = "ENTREE",
                });
            }
            return Ok(result);
        }

        [HttpPut("{idArticle}")]
        public async Task<IActionResult> ModifierArticleStock(Guid idArticle, ArticleStock articleStock)
        {
            if (idArticle != articleStock.IdArticle) return BadRequest();

            return Ok(await _articleService.UpdateAsync(articleStock));
        }

        [HttpDelete("{idArticle}")]
        public async Task<IActionResult> SupprimerArticleStock(Guid idArticle)
        {
            await _articleService.DeleteByIdAsync(idArticle);
            return Ok();
        }
    }
}
