﻿using System;
using BankApplication.Interfaces;
using BankApplication.Logger;
using BankApplication.Logger.Message;

namespace BankApplication.BankClasses
{
    public class SavingsAccount : IBank
    {
        private double _balance;
        private double _rate;
        private double _interest;
        private DateTime _lastInterestCalculatedTime;

        private ILogger _logger;
        private IMessage _message;

        public SavingsAccount(ILogger logger, IMessage message)
        {
            _balance = 0;
            _rate = 4.0;
            _interest = 0;
            _lastInterestCalculatedTime = DateTime.Now;
            _logger = logger;
            _message = message;
        }

        public void Deposit(double amount)
        {
            if (amount <= 0)
                throw new ArgumentException(_message.InvalidAmount());

            _balance += amount;
            _logger.LogMessage(_message.SuccessfulDeposit());
        }

        public void ViewBalance()
        {
            UpdateInterest();
            _logger.LogMessage(_message.ViewBalance(_balance + _interest));
        }

        public void ViewInterest()
        {
            UpdateInterest();
            _logger.LogMessage(_message.ViewInterest(_interest));
        }

        public void Withdraw(double amount)
        {
            UpdateInterest();
            if (amount > _balance + _interest)
                throw new ArgumentException(_message.LowBalance());

            if (amount == 0)
                throw new ArgumentException(_message.InvalidAmount());

            _balance += _interest;
            _interest = 0.0;
            _balance -= amount;
            _logger.LogMessage(_message.WithdrawSuccessful(amount, _balance));
        }

        private void UpdateInterest()
        {
            TimeSpan timeSpan = DateTime.Now - _lastInterestCalculatedTime;
            if(timeSpan.TotalMinutes >= 1)
            {
                int time = (int)timeSpan.TotalMinutes;
                _interest += (_balance * _rate * time) / 100.0;
                _lastInterestCalculatedTime = DateTime.Now;
            }
        }
    }
}
