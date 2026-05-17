using cw10.Data;
using cw10.DTOs;
using cw10.Models;
using Microsoft.EntityFrameworkCore;

namespace cw10.Services;

public class PCService : IPCService
{
     private readonly AppDbContext _context;

        public PCService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<PcResponseDto>> GetAllPcsAsync()
        {
            var pcs = await _context.PCs
                .Select(p => new PcResponseDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Weight = p.Weight,
                    Warranty = p.Warranty,
                    CreatedAt = p.CreatedAt,
                    Stock = p.Stock
                })
                .ToListAsync();

            return pcs;
        }

        public async Task<PcWithComponentsDto?> GetPcWithComponentsAsync(int id)
        {
           
            var pc = await _context.PCs
                .Include(p => p.PCComponents)          
                    .ThenInclude(pc => pc.Component)  
                        .ThenInclude(c => c.Manufacturer) 
                .Include(p => p.PCComponents)
                    .ThenInclude(pc => pc.Component)
                        .ThenInclude(c => c.Type)     
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pc == null)
            {
                return null;
            }

            var result = new PcWithComponentsDto
            {
                Id = pc.Id,
                Name = pc.Name,
                Weight = pc.Weight,
                Warranty = pc.Warranty,
                CreatedAt = pc.CreatedAt,
                Stock = pc.Stock,
                Components = pc.PCComponents.Select(pcComp => new ComponentInPcDto
                {
                    Amount = pcComp.Amount,
                    Component = new ComponentDto
                    {
                        Code = pcComp.Component.Code,
                        Name = pcComp.Component.Name,
                        Description = pcComp.Component.Description,
                        Manufacturer = new ManufacturerDto
                        {
                            Id = pcComp.Component.Manufacturer.Id,
                            Abbreviation = pcComp.Component.Manufacturer.Abbreviation,
                            FullName = pcComp.Component.Manufacturer.FullName,
                            FoundationDate = pcComp.Component.Manufacturer.FoundationDate
                        },
                        Type = new TypeDto
                        {
                            Id = pcComp.Component.Type.Id,
                            Abbreviation = pcComp.Component.Type.Abbreviation,
                            Name = pcComp.Component.Type.Name
                        }
                    }
                }).ToList()
            };

            return result;
        }

        public async Task<PcResponseDto> CreatePcAsync(PcRequestDto request)
        {
            var pc = new PC
            {
                Name = request.Name,
                Weight = request.Weight,
                Warranty = request.Warranty,
                CreatedAt = request.CreatedAt,
                Stock = request.Stock
            };

            _context.PCs.Add(pc);
            await _context.SaveChangesAsync();

            return new PcResponseDto
            {
                Id = pc.Id,
                Name = pc.Name,
                Weight = pc.Weight,
                Warranty = pc.Warranty,
                CreatedAt = pc.CreatedAt,
                Stock = pc.Stock
            };
        }

        public async Task<bool> UpdatePcAsync(int id, PcRequestDto request)
        {
            var pc = await _context.PCs.FindAsync(id);
            
            if (pc == null)
            {
                return false;
            }

            pc.Name = request.Name;
            pc.Weight = request.Weight;
            pc.Warranty = request.Warranty;
            pc.CreatedAt = request.CreatedAt;
            pc.Stock = request.Stock;

            await _context.SaveChangesAsync();
            
            return true;
        }

        public async Task<bool> DeletePcAsync(int id)
        {
            var pc = await _context.PCs.FindAsync(id);
            
            if (pc == null)
            {
                return false;
            }

            _context.PCs.Remove(pc);
            await _context.SaveChangesAsync();
            
            return true;
        }
    }
