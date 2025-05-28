using EFcodefirst.DTOs;

namespace EFcodefirst.Services;

public interface IPrescriptionService
{
    public Task InsertNewPresriptionsAsync(PrescriptionInsertRequestDTO prescription, CancellationToken token);
    public Task<PrescriptionsForClientReplyDTO> GetPrescriptionsAsync(int idPatient, CancellationToken token);
}