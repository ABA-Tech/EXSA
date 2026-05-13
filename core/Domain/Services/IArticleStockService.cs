using Domain.Models;

namespace Domain.Services
{
    public interface IArticleStockService : IAppService<ArticleStock>
    {
    }

    public interface IMouvementStockService : IAppService<MouvementStock>
    {
    }
}
