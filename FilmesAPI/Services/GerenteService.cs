using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.DTO.Gerente;
using FilmesAPI.Models;
using FluentResults;
using System.Collections.Generic;
using System.Linq;

namespace FilmesAPI.Services
{
    public class GerenteService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public GerenteService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadGerenteDto AdicionarGerente(CreateGerenteDto createGerenteDto)
        {
            Gerente gerente = _mapper.Map<Gerente>(createGerenteDto);
            _context.Gerentes.Add(gerente);
            _context.SaveChanges();

            return _mapper.Map<ReadGerenteDto>(gerente);
        }

        public List<ReadGerenteDto> RecuperarGerentes()
        {
            List<ReadGerenteDto> readGerenteDtos = new List<ReadGerenteDto>();
            List<Gerente> gerentes = _context.Gerentes.ToList();

            if (gerentes != null)
            {
                readGerenteDtos = _mapper.Map<List<ReadGerenteDto>>(gerentes);
            }

            return readGerenteDtos;
        }

        public ReadGerenteDto RecuperarGerentesPorId(int id)
        {
            ReadGerenteDto gerenteDto = new ReadGerenteDto();
            Gerente gerente = _context.Gerentes.FirstOrDefault(gerente => gerente.Id == id);

            if (gerente != null)
            {
                gerenteDto = _mapper.Map<ReadGerenteDto>(gerente);
            }

            return gerenteDto;
        }
        
        public Result AtualizarGerente(int id, UpdateGerenteDto updateGerenteDto)
        {
            Gerente gerente = _context.Gerentes.FirstOrDefault(gerente => gerente.Id == id);

            if (gerente == null)
            {
                return Result.Fail("Gerente não encontrado");
            }

            _mapper.Map(updateGerenteDto, gerente);
            _context.SaveChanges();

            return Result.Ok();
        }

        public Result DeletarGerente(int id)
        {
            Gerente gerente = _context.Gerentes.FirstOrDefault(gerente => gerente.Id == id);

            if (gerente == null)
            {
                return Result.Fail("Gerente não encontrado");
            }

            _context.Remove(gerente);
            _context.SaveChanges();

            return Result.Ok();
        }
    }
}
