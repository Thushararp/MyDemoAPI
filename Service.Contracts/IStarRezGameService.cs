using DTOs;

namespace Service.Contracts
{
    public interface IStarRezGameService
    {
        List<AllDto> GetAll(int from, int to);
        bool Validate(ValidateDTO validatePayLoad);

    }
}
