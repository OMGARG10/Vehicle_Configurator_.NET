using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Vehicle_Configurator_Project.Models
{
    [Table("user")]
    public class User
    {
        [Key]
        [Column("userid")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [Column("company_name")]
        public string CompanyName { get; set; }

        [Required]
        [Column("add1")]
        public string Add1 { get; set; }

        [Column("add2")]
        public string Add2 { get; set; }

        [Required]
        [Column("city")]
        public string City { get; set; }

        [Required]
        [Column("state")]
        public string State { get; set; }

        [Required]
        [Column("pin")]
        public int Pin { get; set; }

        [Column("tel")]
        public string Tel { get; set; }

        [Column("fax")]
        public string Fax { get; set; }

        [Required]
        [Column("auth_name")]
        public string AuthName { get; set; }

        [Required]
        [Column("designation")]
        public string Designation { get; set; }

        [Required]
        [Column("auth_tel")]
        public string AuthTel { get; set; }

        [Column("cell")]
        public string Cell { get; set; }

        [Required]
        [Column("company_st_no")]
        public string CompanyStNo { get; set; }

        [Required]
        [Column("company_vat_no")]
        public string CompanyVatNo { get; set; }

        [Column("tax_pan")]
        public string TaxPan { get; set; }

        [Required]
        [Column("email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Column("password")]
        public string Password { get; set; }

        [Column("holding_type")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public HoldingType holding_type { get; set; }

        public enum HoldingType
        {
            Proprietary,
            Pvt_Ltd,
            Ltd
        }

        public override string ToString()
        {
            return $"User [UserId={UserId}, CompanyName={CompanyName}, Add1={Add1}, Add2={Add2}, City={City}, State={State}, Pin={Pin}, Tel={Tel}, Fax={Fax}, AuthName={AuthName}, Designation={Designation}, AuthTel={AuthTel}, Cell={Cell}, CompanyStNo={CompanyStNo}, CompanyVatNo={CompanyVatNo}, TaxPan={TaxPan}, HoldingType={holding_type}, Email={Email}, Password={Password}]";
        }
    }
}
