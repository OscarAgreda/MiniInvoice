using System;
using BlazorShared.Models;

namespace BlazorMauiShared.Models.Customer
{
    public class CreateCustomerRequest : BaseRequest
    {
        public decimal? CreditLimit { get; set; }
        public long? CustomerBigInt { get; set; }
        public byte[] CustomerBinary { get; set; }
        public DateTime? CustomerBirthDate { get; set; }
        public bool CustomerBit { get; set; }
        public string? CustomerChar { get; set; }
        public string? CustomerCompanyName { get; set; }
        public DateTime CustomerDateTime { get; set; }
        public DateTime CustomerDateTime2 { get; set; }
        public decimal? CustomerDecimal { get; set; }
        public string CustomerFirstName { get; set; }
        public double? CustomerFloat { get; set; }
        public string? CustomerGender { get; set; }
        public byte[] CustomerImage { get; set; }
        public int CustomerInt { get; set; }
        public string? CustomerJobTitle { get; set; }
        public string CustomerLastName { get; set; }
        public string? CustomerMiddleName { get; set; }
        public decimal? CustomerMoney { get; set; }
        public string? CustomerNChar { get; set; }
        public string? CustomerNText { get; set; }
        public string? CustomerNVarChar { get; set; }
        public float? CustomerReal { get; set; }
        public short? CustomerSmallInt { get; set; }
        public decimal? CustomerSmallMoney { get; set; }
        public string? CustomerStatus { get; set; }
        public string? CustomerText { get; set; }
        public byte[] CustomerTimestamp { get; set; }
        public sbyte? CustomerTinyInt { get; set; }
        public string? CustomerTitle { get; set; }
        public Guid? CustomerUniqueIdentifier { get; set; }
        public byte[] CustomerVarBinary { get; set; }
        public string? CustomerVarChar { get; set; }
        public string? CustomerWebsite { get; set; }
        public string? CustomerXml { get; set; }
        public string? Industry { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? LastPurchaseDate { get; set; }
        public int? LoyaltyPoints { get; set; }
        public string? Notes { get; set; }
        public string? PreferredContactMethod { get; set; }
        public string? Source { get; set; }
        public Guid TenantId { get; set; }
        public decimal? TotalPurchases { get; set; }
    }
}