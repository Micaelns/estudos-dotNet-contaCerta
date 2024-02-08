using ContaCerta.Domain.Costs.Model;

namespace ContaCerta.Domain.Users.Model
{
    public class UserCost
    {
        public UserCost()
        {
            Id = -1;
            User = new User();
            Cost = new Cost();
            Value = 0;
            Payed = false;
            Payed_at = null;
        }

        public UserCost(User user, Cost cost, float value=0, bool payed = false, DateTime? payed_at=null)
        {
            Id = 0;
            User = user;
            Cost = cost;
            Value = value;
            Payed = payed;
            Payed_at = payed_at;
        }

        public int Id { get; set; }
        public User User { get; set; }
        public Cost Cost { get; set; }
        public float Value { get; set; }
        public bool Payed { get; set; }
        public DateTime? Payed_at { get; set; }
    }
}
