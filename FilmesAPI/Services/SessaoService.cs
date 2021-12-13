using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.DTO.Sessao;
using FilmesAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace FilmesAPI.Services
{
    public class SessaoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public SessaoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadSessaoDto AdicionarSessao(CreateSessaoDto createSessaoDto)
        {
            Sessao sessao = _mapper.Map<Sessao>(createSessaoDto);
            _context.Sessoes.Add(sessao);
            _context.SaveChanges();

            return _mapper.Map<ReadSessaoDto>(sessao);
        }

        public List<ReadSessaoDto> RecuperarSessoes()
        {
            List<ReadSessaoDto> readSessaoDtos = new List<ReadSessaoDto>();
            List<Sessao> sessoes = _context.Sessoes.ToList();

            if (sessoes != null)
            {
                readSessaoDtos = _mapper.Map<List<ReadSessaoDto>>(sessoes);
            }

            return readSessaoDtos;
        }

        public ReadSessaoDto RecuperarSessoesPorId(int id)
        {
            ReadSessaoDto sessaoDto = new ReadSessaoDto();
            Sessao sessao = _context.Sessoes.FirstOrDefault(sessao => sessao.Id == id);

            if (sessao != null)
            {
                sessaoDto = _mapper.Map<ReadSessaoDto>(sessao);
            }

            return sessaoDto;
        }
    }
}
