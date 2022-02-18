using System.Threading.Tasks;

namespace Alex.Model
{
    public interface IService
    {
        Task<string> Trending();
        Task<string> Search(string criteria);
    }
}