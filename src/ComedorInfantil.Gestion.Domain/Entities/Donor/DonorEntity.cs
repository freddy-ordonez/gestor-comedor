using ComedorInfantil.Gestion.Domain.Entities.InKindDonation;
using ComedorInfantil.Gestion.Domain.Entities.MoneyDonation;

namespace ComedorInfantil.Gestion.Domain.Entities.Donor
{
    //Reglas de negocio:
    //Vamos a poder crear un donante desde(POST): /donors
    //Vamos a poder actualizar un donante desde(PUT): /donors/{donorId}
    //Vamos a pode tomar un donante desde(GET): /donors/{donorId}
    //Vamos a pode tomar los donantes desde(GET): /donors
    public class DonorEntity
    {
        public int DonorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DonorType { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public ICollection<MoneyDonationEntity> MoneyDonations { get; set; }
        public ICollection<InKindDonationEntity> InKindDonations { get; set; }
    }
}
