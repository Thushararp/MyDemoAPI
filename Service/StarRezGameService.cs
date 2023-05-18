using DTOs;
using Service.Contracts;

namespace Service
{
    public class StarRezGameService : IStarRezGameService
    {
        public List<AllDto> GetAll(int from, int to)
        {
            List<AllDto> result = new List<AllDto>();

            for (int i = from; i <= to; i++)
            {
                if (i % 3 == 0 && i % 5 == 0)
                {
                    result.Add(new AllDto { Number = i, Return = "StarRez" });
                }
                else if (i % 3 == 0)
                {
                    result.Add(new AllDto { Number = i, Return = "Star" });
                }
                else if (i % 5 == 0)
                {
                    result.Add(new AllDto { Number = i, Return = "Rez" });
                }
                else
                {
                    result.Add(new AllDto { Number = i, Return = i.ToString() });
                }
            }

            return result;
        }

        public bool Validate(ValidateDTO validatePayLoad)
        {
            bool result = false;

            if (validatePayLoad is not null)
            {
                if (validatePayLoad.KidIndex % 3 == 0 && validatePayLoad.KidIndex % 5 == 0 && validatePayLoad.ExpectedReturn == "StarRez")
                {
                    result = true;
                }
                else if (validatePayLoad.KidIndex % 3 == 0 && validatePayLoad.ExpectedReturn == "Star")
                {
                    result = true;
                }
                else if (validatePayLoad.KidIndex % 5 == 0 && validatePayLoad.ExpectedReturn == "Rez")
                {
                    result = true;
                }
                else if (validatePayLoad.KidIndex.ToString() == validatePayLoad.ExpectedReturn)
                {
                    result = true;
                }
            }

            return result;
        }
    }
}
