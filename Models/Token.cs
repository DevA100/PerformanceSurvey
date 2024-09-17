using System.ComponentModel.DataAnnotations.Schema;

namespace PerformanceSurvey.Models
{
    public class Token
    {
        public int Id { get; set; }
        public string AuthToken { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public bool IsValid { get; set; }
        public DateTime IssuedOn { get; set; }
        public DateTime ExpiresOn { get; set; }
        public DateTime? RevokedOn { get; set; }
    }
}
