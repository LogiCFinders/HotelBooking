using ezyvoyagerAPI.Models.DTOs;

using System;
using System.Threading.Tasks;

namespace ezyvoyagerAPI.Repositories.Interfaces
{
    public interface IVoyagerRepository
    {
        Task<VoyagerRESP> GetVoyagerItenary();
        Task<VoyagerItenaryAddREQ> getVoyagerItenary(Int64 voyagerItenaryId);
        Task<VoyagerItenaryAddREQ> AddVoyagerItenary(VoyagerItenaryAddREQ voyagerItenaryREQ);
        Task<LoginRESP> Login(LoginREQ loginREQ);


        //############Static Date ################
        Task<VoyagerStaticDataRESP> GetStaticData(VoyagerStaticDataREQ REQ);
    }
}
