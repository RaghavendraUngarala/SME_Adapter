using SMEAdapter.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.Services
{
    public class CompanyInfoService
    {
        private CompanyDto? _currentCompany;

        public CompanyDto? CurrentCompany
        {
            get => _currentCompany;
            set
            {
                _currentCompany = value;
                NotifyStateChanged();
            }
        }

        public event Action? OnChange;

        public void SetCompany(CompanyDto company)
        {
            _currentCompany = company;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
