﻿using Core.SeedWork;
using FluentValidation;

namespace Core.ValueObjects;

public sealed class CpfVO : BaseValidate
{
    public string? Cpf { get; private set; }

    public CpfVO(string cpf)
    {
        Cpf = string.IsNullOrWhiteSpace(cpf) ? cpf : cpf.Replace(".", "").Replace("-", "").Trim();
        Validate(this, new CpfValidator());

        if (!IsValid) Cpf = default;
    }

    #region Sobrescrita Object

    public override bool Equals(object? obj)
    {
        return obj is CpfVO vO &&
               Cpf == vO.Cpf;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Cpf);
    }

    public static bool operator ==(CpfVO left, CpfVO right) => left.Equals(right);

    public static bool operator !=(CpfVO left, CpfVO right) => !left.Equals(right);

    #endregion Sobrescrita Object

    private class CpfValidator : AbstractValidator<CpfVO>
    {
        public CpfValidator()
        {
            RuleFor(x => x.Cpf)
                .NotNull()
                .Length(11)
                .Must(x => Validar(x));
        }

        private static bool Validar(string cpf)
        {
            cpf = cpf.Replace(".", "").Replace("-", "").Trim();

            if (cpf.Length != 11)
                return false;

            bool igual = true;

            for (int i = 1; i < 11 && igual; i++)
                if (cpf[i] != cpf[0])
                    igual = false;

            if (igual || cpf == "12345678909")
                return false;

            int[] numeros = new int[11];

            for (int i = 0; i < 11; i++)
                numeros[i] = int.Parse(cpf[i].ToString());

            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];

            int resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];

            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
            }
            else if (numeros[10] != 11 - resultado)
                return false;

            return true;
        }
    }
}