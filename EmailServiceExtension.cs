using System.Threading.Tasks;

namespace Mvcday1
{
    public static class EmailServiceExtension
    {
        public static async Task SendRegisterEmail(this IEmailService emailService, string recipentName, string recipientEmail, string phoneNumber)
        {
            await emailService.SendMessage(recipentName, recipientEmail, "ยินดีด้วยลงทะเบียนสำเร็จ", $"คุณได้ลงทะเบียนกับเราสำเร็จแล้ว username ของคุณคือ {recipientEmail} เบอร์โทรศัพท์ของคุณคือ {phoneNumber}");
        }
    }
}