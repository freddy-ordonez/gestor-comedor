using ComedorInfantil.Gestion.Domain.Entities.Donor;

namespace ComedorInfantil.Gestion.Domain.Entities.MoneyDonation
{
    //Reglas de negocio:
    //Vamos a poder tomar todas las donaciones de un donante desde(GET): /donors/{donorId}/money-donation
    //Vamos a poder crear una donacion monetaria de un donante desde(POST): /donors/money-donation
    //Vamos a poder actualizar una donacion monetaria de un donante desde(PUT): /donors/{donorId}/money-donation/{moneyDonationId}
    public class MoneyDonationEntity
    {
        public int MoneyDonationId { get; set; }
        public int DonorId { get; set; }
        public string Porpuse { get; set; }
        public Decimal Amount { get; set; }
        public DateTime DonationDate { get; set; }

        public DonorEntity Donor { get; set; }
    }
}
