using ezyvoyagerAPI.Models.Domain;

namespace ezyvoyagerAPI.Models.DTOs
{
    public class LoginREQ
    {

        public VoyagerActionType Actions { get; set; }
        public LoginREQInfo LoginReq { get; set; }
    }
}