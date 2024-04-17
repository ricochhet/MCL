using System.Threading.Tasks;

namespace MCL.Core.Interfaces.Web;

public interface IDownloadService
{
    public static abstract Task<bool> Download();
}
