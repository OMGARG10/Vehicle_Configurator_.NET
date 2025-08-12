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
        [JsonIgnore]  // Don't allow clients to set this
        public int UserId { get; set; }

        [Required]
        [Column("company_name")]
        [JsonPropertyName("company_name")]
        public string CompanyName { get; set; }

        [Required]
        [Column("add1")]
        [JsonPropertyName("add1")]
        public string Add1 { get; set; }

        [Column("add2")]
        [JsonPropertyName("add2")]
        public string? Add2 { get; set; }  // Nullable

        [Required]
        [Column("city")]
        [JsonPropertyName("city")]
        public string City { get; set; }

        [Required]
        [Column("state")]
        [JsonPropertyName("state")]
        public string State { get; set; }

        [Required]
        [Column("pin")]
        [JsonPropertyName("pin")]
        public int Pin { get; set; }

        [Column("tel")]
        [JsonPropertyName("tel")]
        public string? Tel { get; set; }  // Nullable

        [Column("fax")]
        [JsonPropertyName("fax")]
        public string? Fax { get; set; }  // Nullable

        [Required]
        [Column("auth_name")]
        [JsonPropertyName("auth_name")]
        public string AuthName { get; set; }

        [Required]
        [Column("designation")]
        [JsonPropertyName("designation")]
        public string Designation { get; set; }

        [Required]
        [Column("auth_tel")]
        [JsonPropertyName("auth_tel")]
        public string AuthTel { get; set; }

        [Column("cell")]
        [JsonPropertyName("cell")]
        public string? Cell { get; set; }  // Nullable

        [Required]
        [Column("company_st_no")]
        [JsonPropertyName("company_st_no")]
        public string CompanyStNo { get; set; }

        [Required]
        [Column("company_vat_no")]
        [JsonPropertyName("company_vat_no")]
        public string CompanyVatNo { get; set; }

        [Column("tax_pan")]
        [JsonPropertyName("tax_pan")]
        public string? TaxPan { get; set; }  // Nullable

        [Required]
        [Column("email")]
        [EmailAddress]
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [Required]
        [Column("password")]
        [JsonPropertyName("password")]
        public string Password { get; set; }

        [Column("holding_type")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("holding_type")]
        public HoldingType? Holding_Type { get; set; }  // Nullable enum

        public enum HoldingType
        {
            Proprietary,
            Pvt_Ltd,
            Ltd
        }

        public override string ToString()
        {
            return $"User [UserId={UserId}, CompanyName={CompanyName}, Add1={Add1}, Add2={Add2}, City={City}, State={State}, Pin={Pin}, Tel={Tel}, Fax={Fax}, AuthName={AuthName}, Designation={Designation}, AuthTel={AuthTel}, Cell={Cell}, CompanyStNo={CompanyStNo}, CompanyVatNo={CompanyVatNo}, TaxPan={TaxPan}, HoldingType={Holding_Type}, Email={Email}]";
        }
    }
}
