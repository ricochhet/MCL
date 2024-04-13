using System.Threading.Tasks;

namespace MCL.Core.Interfaces;

public interface IDownloadService
{
    public static abstract Task<bool> Download();
}
