using ComedorInfantil.Gestion.Domain.Entities.Donor;
using ComedorInfantil.Gestion.Domain.Entities.Inventory;

namespace ComedorInfantil.Gestion.Domain.Entities.InKindDonation
{
    //Reglas de Negocio
    //Vamos a poder crear una donacion en especie desde(POST): /inventories/in-kind-donations
    //Vamos a poder tomar una donacion en especie desde(GET): /inventories/{inventoryId}/in-kind-donations
    //Vamos a poder tomar las donaciones en especie de un donante desde(GET): /donors/{donorId}/in-kind-donations
    public class InKindDonationEntity
    {
        public int InKindDonationId { get; set; }
        public int DonorId { get; set; }
        public int ProductId { get; set; }
        public DateTime DonationDate { get; set; }

        public DonorEntity Donor { get; set; }
        public InventoryEntity Inventory { get; set; }
    }
}
