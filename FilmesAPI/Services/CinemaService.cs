﻿using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.DTO.Cinema;
using FilmesAPI.Models;
using FluentResults;
using System.Collections.Generic;
using System.Linq;

namespace CinemasAPI.Services
{
    public class CinemaService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public CinemaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadCinemaDto AdicionarCinema(CreateCinemaDto cinemaDto)
        {
            Cinema cinema = _mapper.Map<Cinema>(cinemaDto);
            _context.Cinemas.Add(cinema);
            _context.SaveChanges();

            return _mapper.Map<ReadCinemaDto>(cinema);
        }

        public List<ReadCinemaDto> RecuperarCinemas(string nomeDoFilme)
        {
            List<ReadCinemaDto> readCinemaDtos = new List<ReadCinemaDto>();
            List<Cinema> cinemas = new List<Cinema>();

            cinemas = _context.Cinemas.ToList();

            if (!cinemas.Any())
            {
                readCinemaDtos = _mapper.Map<List<ReadCinemaDto>>(cinemas);
            }

            if (!string.IsNullOrWhiteSpace(nomeDoFilme))
            {
                IEnumerable<Cinema> query  = from cinema in cinemas
                                             where cinema.Sessoes.Any(sessao => sessao.Filme.Titulo == nomeDoFilme)
                                             select cinema;

                cinemas = query.ToList();
            }

            return readCinemaDtos = _mapper.Map<List<ReadCinemaDto>>(cinemas);
        }

        public ReadCinemaDto RecuperarCinemasPorId(int id)
        {
            ReadCinemaDto cinemaDto = new ReadCinemaDto();
            Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);

            if (cinema != null)
            {
                cinemaDto = _mapper.Map<ReadCinemaDto>(cinema);
            }

            return cinemaDto;
        }

        public Result AtualizarCinema(int id, UpdateCinemaDto cinemaDto)
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);

            if (cinema == null)
            {
                return Result.Fail("Cinema não encontrado");
            }

            _mapper.Map(cinemaDto, cinema);
            _context.SaveChanges();

            return Result.Ok();
        }

        public Result DeletarCinema(int id)
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);

            if (cinema == null)
            {
                return Result.Fail("Cinema não encontrado");
            }

            _context.Remove(cinema);
            _context.SaveChanges();
            
            return Result.Ok();
        }
    }
}
