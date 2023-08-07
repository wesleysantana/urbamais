﻿using Core.Domain;
using Core.ValueObjects;

namespace Urbamais.Domain.Entities.EntitiesOfCore;

public class Cidade : CidadeCore
{
    public IEnumerable<Endereco>? Endereco { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    protected Cidade()
    { }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Cidade(string idUserCreation, Nome nome, Uf uf) : base(idUserCreation, nome, uf)
    {
    }
}