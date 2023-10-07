namespace Vault;

public struct AccessReason
{
    public Reason Reason { get; }

    public string OtherMessage { get; }
    
    private AccessReason(Reason reason, string otherMessage = "")
    {
        Reason = reason;
        OtherMessage = otherMessage;
    }
    
    public static AccessReason AppFunctionality => new(Reason.AppFunctionality);
    public static AccessReason Analytics => new(Reason.Analytics);
    public static AccessReason Notifications => new(Reason.Notifications);
    public static AccessReason Marketing => new(Reason.Marketing);
    public static AccessReason ThirdPartyMarketing => new(Reason.ThirdPartyMarketing);
    public static AccessReason FraudPreventionSecurityAndCompliance => new(Reason.FraudPreventionSecurityAndCompliance);
    public static AccessReason AccountManagement => new(Reason.AccountManagement);
    public static AccessReason Maintenance => new(Reason.Maintenance);
    public static AccessReason DataSubjectRequest => new(Reason.DataSubjectRequest);
    public static AccessReason Other(string other) => new(Reason.Other, other);

}