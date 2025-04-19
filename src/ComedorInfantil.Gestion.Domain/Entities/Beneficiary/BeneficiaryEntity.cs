namespace ComedorInfantil.Gestion.Domain.Entities.Beneficiary
{
    //Reglas de negocio:
    //Vamos a poder crear un beneficiario desde(POST): /beneficiaries
    //Vamos a poder actualizar un beneficiario desde(PUT): /beneficiaries/{beneficiaryId}
    //Vamos a pode tomar un beneficiario desde(GET): /beneficiaries/{beneficiaryId}
    //Vamos a pode tomar los beneficiarios desde(GET): /beneficiaries
    public class BeneficiaryEntity
    {
        public int BeneficiaryId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public String Status { get; set; }
    }
}
