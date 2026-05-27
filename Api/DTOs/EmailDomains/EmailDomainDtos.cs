namespace Api.DTOs.EmailDomains;

public sealed record CreateEmailDomainRequest(string Domain);
public sealed record EmailDomainResponse(int Id, string Domain);
