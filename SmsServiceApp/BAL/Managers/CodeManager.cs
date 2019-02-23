﻿using System;
using System.Collections.Generic;
using System.Text;
using WebCustomerApp.Models;
using Model.Interfaces;
using Model.ViewModels.CodeViewModels;
using AutoMapper;
using System.Linq;

namespace BAL.Managers
{
    public class CodeManager : BaseManager, ICodeManager
    {
        public CodeManager(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }
        public bool Add(CodeViewModel NewCode)
        {
            var result = mapper.Map<Code>(NewCode);
            try
            {
                unitOfWork.Codes.Insert(result);
                unitOfWork.Save();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public CodeViewModel GetById(int Id)
        {
            var oper = unitOfWork.Codes.GetById(Id);
            return mapper.Map<CodeViewModel>(oper);
        }

        public int GetNumberOfPages(int OperatorId, int NumOfElements = 20)
        {
            return (unitOfWork.Codes.Get(o => o.OperatorId == OperatorId)
                .Count() / (NumOfElements + 1)) + 1;
        }

        public IEnumerable<CodeViewModel> GetPage(int OperatorId, int Page = 1, int NumOfElements = 20)
        {
            var codes = unitOfWork.Codes.Get(o => o.OperatorId == OperatorId,
                o => o.OrderBy(s => s.OperatorCode));
            codes = codes.Skip(NumOfElements * (Page - 1)).Take(NumOfElements);
            var result = mapper.Map<IEnumerable<Code>, IEnumerable<CodeViewModel>>(codes);
            return result;
        }

        public bool Remove(int Id)
        {
            var code = unitOfWork.Codes.GetById(Id);
            if (code == null)
                return false;
            try
            {
                unitOfWork.Codes.Delete(code);
                unitOfWork.Save();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool Update(CodeViewModel UpdatedCode)
        {
            var result = mapper.Map<Code>(UpdatedCode);
            try
            {
                unitOfWork.Codes.Update(result);
                unitOfWork.Save();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
