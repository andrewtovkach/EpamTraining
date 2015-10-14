using System;
using Transport.Enums;

namespace Transport.Model
{
    class Locomotive : Vehicle
    {
        public TypeEnergy TypeEnergy { get; set; }
        public uint Power { get; set; }
        public uint MaxSpeed { get; set; }

        public Locomotive(int number, DateTime startUpDate, uint power, TypeEnergy typeEnergy, uint maxSpeed)
            : base(number, startUpDate)
        {
            this.TypeEnergy = typeEnergy;
            this.Power = power;
            this.MaxSpeed = maxSpeed;
        }

        public override string ToString()
        {
            return String.Format("Локомотив {0} Тип энергии: {1}, Мощность: {2}, Макс. скорость: {3}", base.ToString(), 
                TypeEnergy, Power, MaxSpeed);
        }
    }
}
