namespace BoscoAFH.Common
{
    public enum APIHttpType
    {
        HttpGet = 1,
        HttpPost = 2,
        HttpDelete = 3,
        HttpPut = 4
    }

    public static class ErrorCodes
    {
        public const int Success = 200;
        public const int Created = 201;
        public const int Updated = 202;
        public const int Failed = 203;
        public const int NoRecordFound = 204;
        public const int Deleted = 205;
        public const int Exist = 206;
        public const int InValidToken = 207;

        public const int CustomMessage = 102;
        public const int EquipmentNameExist = 103;
        public const int RejectReasonEmpty = 105;

        public const int BadRequest = 400;
        public const int UnAuthorized = 401;
        public const int Authorized = 104;
        public const int RequestTimeOut = 408;
        public const int ServiceTimeout = 440;
        public const int InvalidCredentials = 422;
        public const int PreconditionFailed = 412;
        public const int MemberScheduleAlready = 100;
        public const int MemberSchedule = 101;
        public const int KHIDAlreadyExist = -98;
        public const int CaseNumberAlreadyExist = -97;
        public const int AlreadyExist = -99;
        public const int NoRowsAffected = 0;
        public const int NotExist = -1;
        public const int UserInactive = -91;
        public const int InternalServerError = 500;
        public const int UnsupportedMediaType = 415;
        public const int Conflict = 409;
        public const int NotFound = 404;
        public const int ConflictDesc = 410;
        public const int ConflictCodeDesc = 411;
        public const int AlreadyApproved = 300;
        public const int AlreadyRejected = 301;
        public const int AlreadyClosed = 302;
        public const int CodeExistsButDeactivated = 413;
        public const int CodeAndDescriptionExistButDeactivated = 414;
        public const int MealLimitExceeded = 3001;
        public const int InvalidMealType = 3002;
        public const int MealTypeAlreadyOrdered = 3003;
        public const int InsufficientWallet = 3004;
        public const int InsufficientBalance = 3005;
        public const int StaffTypeNotFound = 3006;
        public const int TransferIDNotFound = 3007;
        public const int scanMealTypeAlreadyOrdered = 208;
        public const int ItemQtyNotAvailable = 460;
        public const int RequestQtyNotAvailable = 461;
        public const int AlreadyCreated = 462;
        public const int AlreadyCancelled = 463;
        public const int RecordChanged = 464;
        public const int AlreadyPosted = 465;
        public const int ItemConsumedNotAvailable = 466;
        public const int ConsumedNotAvailable = 467;
        public const int ConcurrencyConflict = -1;
        public const int AlreadyValidated = -2;
        public const int StatusChanged = 466;
        public const int POCancelled = 476;
        public const int PSRstarted = 111;
        public const int InvalidTimeRange = 209;
    }

    public static class ErrorMessages
    {
        public const string Success = "Success";
        public const string RemoveSuccess = "Removed Successfully.";
        public const string Restored_Success = "Restored Successfully.";
        public const string AttendanceDeleted = "Deleted Successfully.";
        public const string NoClassesSelected = "No class are selected.";
        public const string NoAssignmentSelected = "No assignments are selected.";
        public const string NoAttendanceSelected = "No attendance are selected.";
        public const string Failed = "Failed";
        public const string Exist = "Record already exists.";
        public const string QuestionExist = "This Question already exists.";
        public const string TitleExist = "This Title already exists.";
        public const string Error = "An error occurred.";
        public const string MenuIdExist = "Menu Id already exists";
        public const string MealsExist = "Meals already exists";
        public const string EmailExist = "Email already exists";
        public const string UsernameExist = "Username already exists";
        public const string ListSuccess = "Success";
        public const string TemplateFileNotFound = "Template file not found";
        public const string UnknownError = "Unknown Error";
        public const string Undefined = "Undefined Message";
        public const string ListFailed = "Unable to get the record.";
        public const string InvaildLogin = "Invaild Username and Password";

        public const string CancelExist = "Already Canceled";
        public const string ConfirmExist = "The records have been updated already.";
        public const string ConflictExist = "The record has been modified by another user.";
        public const string GenerateExist = "The worklist has been generated already.";

        public const string UserNameNotExists = "Username is invalid";
        public const string PasswordIncorrect = "Password is invalid";
        public const string UserNameandPasswordIncorrect = "Invalid username or password. Please try again";
        public const string InActiveUser = "Your account has been deactivated";
        public const string ExistUser = "User already exists";
        public const string InCompleteUser = "Your account has not been completed";
        public const string InValidRoleSwitch = "Invalid Role switch has given!";
        public const string InValidCredentials = "Invalid Credentials";
        public const string NoAccess = "Access Denied for this user";

        public const string SaveSuccess = "Saved Successfully.";
        public const string SubmittedSuccess = "Submitted Successfully.";
        public const string UpdateSuccess = "Updated Successfully.";
        public const string StartSuccess = "Started successfully.";
        public const string StopSuccess = "Stopped successfully.";
        public const string FetchImportPageFailed = "Unable to fetch import page.";
        public const string UpdateFailed = "Unable to Updated record.";
        public const string CancelSuccess = "Cancelled successfully.";
        public const string SentforReviewSuccess = "Send for Department Review Successfully.";
        public const string ReviewSuccess = "Reviewed Successfully.";
        public const string ReviewSubmitSuccess = "Submitted for review successfully.";
        public const string UnCancelSuccess = "Uncancelled successfully.";
        public const string DuplicateSuccess = "Duplicated Successfully.";
        public const string GenerateSuccess = "Generated successfully.";
        public const string AlertReviewed = "Alert reviewed successfully.";
        public const string BlockSuccess = "Blocked successfully.";
        public const string UnblockSuccess = "Unblocked successfully.";

        public const string ActiveSuccess = "Activated Successfully.";
        public const string ActiveFailed = "Activation failed.";
        public const string DeactivateSuccess = "Deactivated Successfully.";
        public const string DeactivateFailed = "Deactivation failed.";
        public const string AlertReviewFailed = "Alert review Failed.";
        public const string OrderReviewSuccess = "Order review successfully.";
        public const string AuthorizeSuccess = "Authorized successfully.";
        public const string StatusSuccess = "Status updated success.";
        public const string SaveFailed = "Unable to save record.";
        public const string NotFound = "Record not found.";

        public const string DeleteSuccess = "Deleted Successfully.";
        public const string DeleteFailed = "Unable to delete record.";
        public const string AlreadyDeleted = "Record is already deleted.";

        public const string NoRecordFound = "No record found.";
        public const string IdIsNotFound = "Given id is not found.";
        public const string RecordReferred = "Record referred.";
        public const string RescheduleSlotNotAvailable = "Selected time slot is not available";
        public const string PaymentSuccess = "Payment confirmed successfully.";

        public const string OTPSuccess = "OTP sent successfully.";
        public const string OTPFailed = "Failed to send OTP. Please try again";

        public const string InValidKey = "Invalid key";
        public const string InValidToken = "Invalid token";

        public const string InValidFileFormt = "Invalid file format. Only Excel files are allowed.";
        public const string InValidWorksheet = "The Excel file does not contain required worksheets.";
        public const string InValidHeader = "The Excel file has incorrect item headers.";
        public const string InValidData = "The Excel file does not contain any item data.";
        public const string InValidDataRow = "Invalid data in row.";
        public const string NoFileUploaded = "No file uploaded.";
        public const string NoItems = "No items to import.";
        public const string InValidExcelFormat = "Excel format is not correct";
        public const string SaintIdRequired = "SaintId is required.";

        public const string InvalidFile = "Invalid file has been given.";
        public const string FileNotSaved = "Unable to save the file.";
        public const string MovedStock = "Moved successfully.";
        public const string MovedPurchase = "Moved successfully.";
        public const string Approved = "Approved successfully.";
        public const string AlreadyApproved = "Already Approved.";
        public const string AlreadyAuthorized = "Purchase Order has already been authorized.Please review  the changes.";
        public const string AlreadyPosted = "Purchase Order has already been Posted.Please review  the changes.";
        public const string AlreadyRecordRejected = "Purchase Order has already been rejected.Please review  the changes.";
        public const string Rejected = "Rejected successfully.";
        public const string RejectedFalid = "Rejected faild.";
        public const string AlreadyRejected = "Already Rejected.";

        public const string EmailFailed = "Failed to send email.";
        public const string PaymentLinkFailed = "Failed to send payment link.";
        public const string EmailSent = "Email sent successfully.";
        public const string SentToVendorStatus = "Status has been successfully updated to Sent to Vendor.";
        public const string PaymentLinkSent = "Payment link sent successfully.";
        public const string InvalidOTP = "Invalid OTP.";
        public const string OldPasswordIncorrect = "Old Password is incorrect";
        public const string SamePassword = "New Password is the same as the Old Password";
        public const string InvalidImage = "Invalid image file.";
        public const string OTPExpired = "OTP Expired.";
        public const string InvalidEmail = "Invalid email address.";
        public const string EmailNotFound = "Email not found.";
        public const string InternalServerError = "Internal server error.";
         
    }

    public static class SerilogErrorMessages
    {
        public const string GetFailed = "The records retrieval failed.";
    }

    public static class InfoMessages
    {
        public const string ActivationSuccess = "Activated Successfully.";
        public const string AreadyActivaded = "This system is already activated with a productkey.";
        public const string InvalidKey = "Product Key is not valid.";
    }

    public static class DefaultPage
    {
        public const string WebStart = @"<html>
        <head>
            <style>
                body {
                    font-family: Arial, sans-serif;
                    margin: 0;
                    padding: 0;
                    display: flex;
                    justify-content: center;
                    align-items: center;
                    height: 100vh;
                    background-color: #f0f0f0;
                }
                .container {
                    text-align: center;
                    background-color: #fff;
                    padding: 50px;
                    border-radius: 10px;
                    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                }
                h1 {
                    color: #333;
                }
            </style>
        </head>
        <body>
            <div class='container'>
                <h1>Welcome to {0} Service!</h1> <br/>
<a   href='/swagger'>Swagger UI</a>
            </div>

        </body>
    </html>";

        public const string Home = "Home";
    }
}
