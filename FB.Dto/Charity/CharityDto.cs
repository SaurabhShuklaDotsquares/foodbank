using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FB.Dto
{
    public class CharityDto
    {
        public int CharityID { get; set; }

        [DisplayName("Organisation")]
        public int CentralOfficeID { get; set; }

        [DisplayName("Charity Name")]
        public string CharityName { get; set; }


        [DisplayName("Charity Prefix")]
        public string Prefix { get; set; }

        [DisplayName("Active")]
        public bool IsActive { get; set; }

        public int CreatedBy { get; set; }
        public int AuditUserId { get; set; }
        public string AuditIP { get; set; }

        [DisplayName("MyMembership.Online")]
        public bool IsMMO { get; set; }

        [DisplayName("MyGiving.Online")]
        public bool IsMGO { get; set; }
        public bool IsMmosystemCreated { get; set; }
        [DisplayName("Third Party Processing Amount Limit")]
        public Nullable<decimal> TPDonationAmountLimit { get; set; }
        [DisplayName("GoCardLess Access Token")]
        public string GoCardLessAccessKey { get; set; }
        [DisplayName("GoCardLess Merchant ID")]
        public int? GoCardLessVerificationStatus { get; set; }
        public string GoCardLessVerificationUrl { get; set; }
        public string GoCardLessMerchantID { get; set; }

        [DisplayName("GoCardLess App Identifier")]
        public string GoCardLessAppID { get; set; }

        [DisplayName("GoCardLess App Secret Key")]
        public string GoCardLessAppSecret { get; set; }
        [DisplayName("Braintree Merchant Id")]
        public string BraintreeMerchantId { get; set; }
        [DisplayName("Braintree Merchant Account ID (optional but required for PayPal)")]
        public string BraintreeAccountMerchantId { get; set; }
        [DisplayName("PayPal enabled on Braintree")]
        public bool IsBraintreePayPalEnabled { get; set; }
        [DisplayName("Braintree Public Key")]
        public string BraintreePublicKey { get; set; }
        [DisplayName("Braintree Private Key")]
        public string BraintreePrivateKey { get; set; }
        [DisplayName("Paypal Currency")]
        public int PaypalCurrency { get; set; }
        public bool IncludeTransactionFeeForBrainTree { get; set; }
        public bool IncludeTransactionFeeForPAC { get; set; }

        [DisplayName("BrainTree Donate Now Redirect Url")]
        public string BrainTreeDonateRedirectUrl { get; set; }
        [DisplayName("Enable one time email verification requirement")]
        public bool EnableBTEmailVerification { get; set; }
        [DisplayName("Enable Maximum donations per day per email")]
        public bool EnableNoOfBTEmailDonation { get; set; }
        [DisplayName("Maximum donations per day per email")]
        public Nullable<int> AllowNoOfBTEmailDonation { get; set; }
        public int FailedBTDonationLimit { get; set; }
        public Nullable<decimal> TransactionFeeAmountForBrainTree { get; set; }
        public int EnrolmentFormId { get; set; }

        public string OauthFlowUrl { get; set; }
        [DisplayName("Donate Regularly - Return URL")]
        public string DonateRegularlyReturnURL { get; set; }
        public Nullable<decimal> TransactionFeeAmount { get; set; }
        public bool IncludeTransactionFee { get; set; }

        [DisplayName("Mail Chimp Token")]
        public string MailChimpToken { get; set; }
    }
}
