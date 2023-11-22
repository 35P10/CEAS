using FrontEnd.Models;

namespace FrontEnd.Services.Contracts
{
    public interface ICEAS
    {
        Task<SintaxisModel> CheckSintaxisAsync(int idCode, string code);
        Task<RunModel> RunCompilerAsync(int idCode, string code);
    }
}
