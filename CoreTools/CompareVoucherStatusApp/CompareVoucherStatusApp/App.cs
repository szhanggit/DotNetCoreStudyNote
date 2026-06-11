using CompareVoucherStatusApp.Entities;
using CompareVoucherStatusApp.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompareVoucherStatusApp
{
    public class App : IApp
    {
        private ISerilogService _log = null;
        private IVoucherRepository _repository = null;
        private IExtendVoucherRepository _eRepository = null;
        private List<MoveVoucherNumberProgramCodeClientName> _inputVoucherNumberProgramCodeList = null;
        private List<VoucherMove> _updateList = null;
        private ISetting _setting = null;
        private int Step = 3000;

        public App(
            ISerilogService log
            , IVoucherRepository repository
            , IExtendVoucherRepository eRepository
            , ISetting setting
            )
        {
            this._log = log;
            this._repository = repository;
            this._eRepository = eRepository;
            this._inputVoucherNumberProgramCodeList = new List<MoveVoucherNumberProgramCodeClientName>();
            this._updateList = new List<VoucherMove>();
            this._setting = setting;
        }

        public void Run()
        {
            _log.logWarning("Start Running App");
            int RunningStep = _setting.RunningStep;

            /*do
            {
                _log.logWarning("Start Inner Loop");
                _inputVoucherNumberProgramCodeList = _eRepository.GetExtendVoucherSyncInfo(Step);
                _updateList = _repository.GetVoucherStatusFromMove(_inputVoucherNumberProgramCodeList);
                _eRepository.UpdateVoucherStatus(_updateList);
                _log.logWarning("End Inner Loop");
            } while (_inputVoucherNumberProgramCodeList.Count > 0);*/

            do
            {
                _log.logWarning("Start Inner Loop");
                _inputVoucherNumberProgramCodeList = _eRepository.GetExtendVoucherSyncInfo2(Step);
                _repository.BulkInsertVoucherBuff(_inputVoucherNumberProgramCodeList, "TempVoucher20220311", "MoveConnection");
                _log.logWarning("End Inner Loop");
            } while (_inputVoucherNumberProgramCodeList.Count > 0);

            _log.logWarning("Finish Running App");
            return;
        }
    }
}
