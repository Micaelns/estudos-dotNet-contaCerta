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
        }

        public Cost(string title, string description, float value, User userRequested, bool active = true)
        {
            Title = title;
            Description = description;
            Value = value;
            Active = active;
            CreatedAt = DateTimeOffset.Now;
            UserRequested = userRequested;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public float Value { get; set; }
        public bool Active { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public User UserRequested { get; set; }
    }
}