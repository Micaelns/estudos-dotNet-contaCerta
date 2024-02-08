using ContaCerta.Domain.Users.Model;

namespace ContaCerta.Domain.Costs.Model
{
    public class Cost
    {
        public Cost() {
            Title = "";
            Description = "";
            Value = 0;
            Active = false;
            CreatedAt = null;
            UserRequested = new User();
            UserCosts = [];
        }

        public Cost(string title, string description, float value, DateTime paymentDate , User userRequested, bool active = true)
        {
            Title = title;
            Description = description;
            Value = value;
            PaymentDate = paymentDate;
            Active = active;
            CreatedAt = DateTimeOffset.Now;
            UserRequested = userRequested;
            UserCosts = [];
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public float Value { get; set; }
        public DateTime? PaymentDate { get; set; }
        public bool Active { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public User UserRequested { get; set; }
        public List<UserCost> UserCosts { get; set; }
    }
}