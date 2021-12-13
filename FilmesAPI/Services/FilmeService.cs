using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.DTO.Filme;
using FilmesAPI.Models;
using FluentResults;
using System.Collections.Generic;
using System.Linq;

namespace FilmesAPI.Services
{
    public class FilmeService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public FilmeService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadFilmeDto AdicionarFilme(CreateFilmeDto createFilmeDto)
        {
            Filme filme = _mapper.Map<Filme>(createFilmeDto);
            _context.Filmes.Add(filme);
            _context.SaveChanges();

            return _mapper.Map<ReadFilmeDto>(filme);
        }

        public List<ReadFilmeDto> RecuperarFilmes(int? classificacaoEtaria)
        {
            List<Filme> filmes = new List<Filme>();
            List<ReadFilmeDto> readFilmeDtos = new List<ReadFilmeDto>();

            if (classificacaoEtaria == null)
            {
                filmes = _context.Filmes.ToList();
            }
            else
            {
                filmes = _context
                    .Filmes.Where(filme => filme.ClassificacaoEtaria <= classificacaoEtaria).ToList();
            }

            if (!filmes.Any())
            {
                readFilmeDtos = _mapper.Map<List<ReadFilmeDto>>(filmes);
            }

            return readFilmeDtos;
        }

        public ReadFilmeDto RecuperarFilmesPorId(int id)
        {
            ReadFilmeDto readFilmeDto  = new ReadFilmeDto();
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

            if (filme != null)
            {
                readFilmeDto = _mapper.Map<ReadFilmeDto>(filme);
            }

            return readFilmeDto;
        }

        public Result AtualizarFilme(int id, UpdateFilmeDto updateFilmeDto)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

            if (filme == null)
            {
                return Result.Fail("Filme não encontrado");
            }

            _mapper.Map(updateFilmeDto, filme);
            _context.SaveChanges();

            return Result.Ok();
        }

        public Result DeletarFilme(int id)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

            if (filme == null)
            {
                return Result.Fail("Filme não encontrado");
            }

            _context.Remove(filme);
            _context.SaveChanges();

            return Result.Ok();
        }
    }
}
