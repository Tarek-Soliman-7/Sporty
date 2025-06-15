namespace Sporty.Enums
{
    public enum BookingStatus
    {
        Confirmed,
        Cancelled
    }

    public enum MembershipStatus
    {
        Active,
        Expired,
        Cancelled,
        Suspended
    }
    public enum PaymentOption
    {
        FullPayment = 1,
        Installments = 2,
        Free = 3
    }
    public enum Gender
    {
        Male,
        Female,
    }

    public enum PaymentMethod
    {
        Card,
    }

    public enum PaymentRelatedTo
    {
        Event,
        Membership
    }

    public enum PaymentStatus
    {
        Paid,
        Failed,
        Refunded

    }

    public enum PayoutStatus
    {
        Pending,
        Approved,
        Rejected,
        Failed
    }

    public enum MembershipRequestStatus
    {
        Pending = 0,
        Accepted = 1,
        Rejected = 2
    }
}
