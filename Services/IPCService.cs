using cw10.DTOs;

namespace cw10.Services;

public interface IPCService
{
    Task<List<PcResponseDto>> GetAllPcsAsync();
        
    Task<PcWithComponentsDto?> GetPcWithComponentsAsync(int id);
        
    Task<PcResponseDto> CreatePcAsync(PcRequestDto request);
        
    Task<bool> UpdatePcAsync(int id, PcRequestDto request);
        
    Task<bool> DeletePcAsync(int id);
}