using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.DTO.Endereco;
using FilmesAPI.Models;
using FluentResults;
using System.Collections.Generic;
using System.Linq;

namespace FilmesAPI.Services
{
    public class EnderecoService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public EnderecoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadEnderecoDto AdicionarEndereco(CreateEnderecoDto createEnderecoDto)
        {
            Endereco endereco = _mapper.Map<Endereco>(createEnderecoDto);
            _context.Enderecos.Add(endereco);
            _context.SaveChanges();

            return _mapper.Map<ReadEnderecoDto>(endereco);
        }

        public List<ReadEnderecoDto> RecuperarEnderecos()
        {
            List<ReadEnderecoDto> readEnderecoDtos = new List<ReadEnderecoDto>();
            List<Endereco> enderecos = _context.Enderecos.ToList();

            if (enderecos != null)
            {
                readEnderecoDtos = _mapper.Map<List<ReadEnderecoDto>>(enderecos);
            }

            return readEnderecoDtos;
        }

        public ReadEnderecoDto RecuperarEnderecosPorId(int id)
        {
            ReadEnderecoDto readEnderecoDto = new ReadEnderecoDto();
            Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);

            if (endereco != null)
            {
                readEnderecoDto = _mapper.Map<ReadEnderecoDto>(endereco);
            }

            return readEnderecoDto;
        }

        public Result AtualizarEndereco(int id, UpdateEnderecoDto updateEnderecoDto)
        {
            Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);

            if (endereco == null)
            {
                return Result.Fail("Endereço não encontrado");
            }

            _mapper.Map(updateEnderecoDto, endereco);
            _context.SaveChanges();

            return Result.Ok();
        }

        public Result DeletarEndereco(int id)
        {
            Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);

            if (endereco == null)
            {
                return Result.Fail("Endereço não encontrado");
            }

            _context.Remove(endereco);
            _context.SaveChanges();

            return Result.Ok();
        }
    }
}
