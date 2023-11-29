namespace FB.Web
{
    public static class Constants
    {
        public const decimal GoCardlessTransactionFeeVAT = 1.20255m;

        #region CustomErrorMessage
        public const string CustomErrorMessage = "Something went wrong. Please come back later.";
        public const string UnAuthorizedMessage = "Un-Authorize access, access denied, please come back later.";
        public const string CustomRequiredErrorMessage = "Please fill all required fields!";
        public const string ValidGASDSForEditDelete = "The requested change cannot be completed as it will result in GASDs eligible donations to fall below what has already been claimed with the HMRC.  A GASDS refund will need to be sent to the HMRC before the donation can be edited/deleted.  Refunds can be created under the claims menu and sent to the HMRC as part of your next claim.";
        public const string InvailGraphClient = "Could not create a graph client, Please add valid Microsoft 365 access details like Client ID, Tenant ID & Secret Code";
        public const string Authorization_IdentityNotFound = "Your tenant ({0}) is not connected with Microsoft 365 App ({1}), So Please go on your profile page and enable your tenant id with Microsoft 365 App.";
        public const string Authorization_RequestDenied = "Insufficient privileges in your Microsoft 365 App ({0}), Ask App administrator to add privileges on same and also you will go on your profile page and enable your tenant id with Microsoft 365 App.";
        #endregion

        public const string DefaultCountry = "United Kingdom";
        public static string vbCrLf = System.Environment.NewLine;

        public const string VerificationDetailsNotFound = "Verification Email Details not found";
        public const string GoCardlessErrorKeyword = "Gocardless :";
        public const string DeleteDonateNowButtons = "Record for the Button Ids @ButtonIds removed";

        #region Table constants
        public const string CharityTable = "Charity";
        public const string BranchTable = "Branch";
        #endregion

        #region Activity Log Messages
        public const string ImportData = "Import started with file {0} to {1} existing data at {2} level on date {3}";
        public const string UploadImportData = "Import uploaded with file {0} to {1} existing data at {2} level on date {3}";
        #endregion

        #region Add None List
        public const string NoneText = "None";
        public const string NoneStringValue = "";
        public const string NoneIntValue = "0";
        #endregion
    }
}
