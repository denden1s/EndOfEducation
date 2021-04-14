using Computer_house.DataBase.Entities;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Computer_house.OtherClasses
{
    static class SystemFunctions
    {

        //Нужен для настройки ip-адреса
        public static void SetNewDataBaseAdress()
        {
            string newIP = Microsoft.VisualBasic.Interaction.InputBox("Введите IP-адрес сервера БД:",
                "Установка IP");
            SetupIP setIP = new SetupIP(newIP);
            setIP.ChangeXmlFile();
        }


        //Можно использовать общий метод выделив массивы в главной форме

        //Раздел очистки полей
        public static void ClearCoolingSystemTextBoxes(ComponentsOptionsForm componentsOptions)
        {
            TextBox[] textBoxes = { 
                componentsOptions.CoolingSystemIDTextBox, componentsOptions.CoolingSystemNameTextBox,
                componentsOptions.CoolingSystemSocketsTextBox, componentsOptions.CoolingSystemCountOfHeatPipesTextBox,
                componentsOptions.CoolingSystemTypeOfBearing, componentsOptions.CoolingSystemNoiseLevelTextBox,
                componentsOptions.CoolingSystemMaxSpeedTextBox, componentsOptions.CoolingSystemConsumptionTextBox,
                componentsOptions.CoolingSystemDiametrTextBox
            };
            ComboBox[] comboBoxes = { componentsOptions.CoolingSystemPowerTypeComboBox };
            CheckBox[] checkBoxes = { 
                componentsOptions.CoolingSystemEvaporationChamberCheckBox, componentsOptions.CoolingSystemRSCCheckBox};
            Clear(textBoxes, comboBoxes, checkBoxes);
        }

        public static void ClearRAMTextBoxes(ComponentsOptionsForm componentsOptions)
        {
            TextBox[] textBoxes = { componentsOptions.RAMIDTextBox, componentsOptions.RAMNameTextBox,
                componentsOptions.RAMKitTextBox, componentsOptions.RAMVoltageTextBox,
                componentsOptions.RAMCapacityTextBox, componentsOptions.RAMTimingsTextBox
            };
            ComboBox[] comboBoxes = { componentsOptions.RAMTypeComboBox,componentsOptions.RAMFrequencyComboBox};
            CheckBox[] checkBoxes = { componentsOptions.RAMLowProfileModuleCheckBox, componentsOptions.RAMCoolingCheckBox,
                componentsOptions.RAMxmpSupportCheckBox
            };
            Clear(textBoxes, comboBoxes, checkBoxes);
        }
        public static void ClearMotherboardTextBoxes(ComponentsOptionsForm componentsOptions)
        {
            TextBox[] textBoxes = { componentsOptions.MotherboardIDTextBox, componentsOptions.MotherboardNameTextBox,
                componentsOptions.MotherboardConnectorsTextBox, componentsOptions.MotherboardSupportedCPUTextBox,
                componentsOptions.MotherboardRAMCapacityTextBox,componentsOptions.MotherboardCountOfRAMSlotsTextBox,
                componentsOptions.MotherboardExpansionsSlotsTextBox, componentsOptions.MotherboardStorageInterfacesTextBox,
                componentsOptions.MotherboardLengthTextBox, componentsOptions.MotherboardWidthTextBox
            };
            ComboBox[] comboBoxes = { componentsOptions.MotherboardSocketComboBox,componentsOptions.MotherboardRamFrequencyComboBox,
            componentsOptions.MotherboardChipsetComboBox,componentsOptions.MotherBoardRAMChanelsComboBox,
            componentsOptions.MotherboardFormFactorComboBox, componentsOptions.MotherboardSupportedRAMComboBox};
            componentsOptions.MotherboardIntegratedGraphicCheckBox.Checked = false;
            componentsOptions.MotherboardSLISupportCheckBox.Checked = false;
            Clear(textBoxes, comboBoxes);
        }

        public static void ClearCaseTextBoxes(ComponentsOptionsForm componentsOptions)
        {
            TextBox[] textBoxes = 
            {
                componentsOptions.CaseIDTextBox,componentsOptions.CaseNameTextBox, componentsOptions.CasePSUTextBox,
                componentsOptions.CasePSUPositionTextBox, componentsOptions.CaseMaterialTextBox, 
                componentsOptions.CaseSupportedMotherboardsTextBox, componentsOptions.CaseCoolingTypeTextBox,
                componentsOptions.CaseCoolersCountTextBox, componentsOptions.CaseCoolerSlotsCountTextBox,
                componentsOptions.CaseHeightTextBox, componentsOptions.CaseWidthTextBox, componentsOptions.CaseDepthTextBox,
                componentsOptions.CaseStorageLocationCountTextBox, componentsOptions.CaseExpansionSlotsCount,
                componentsOptions.CaseMaxLengthOfGPUTextBox, componentsOptions.CaseMaxCPUCoolerHeightTextBox,
                componentsOptions.CaseMaxPSULengthTextBox, componentsOptions.CaseWeightTextBox
            };


            componentsOptions.CaseFormFactorComboBox.SelectedItem = null;
            Clear(textBoxes);
            componentsOptions.CaseWaterCoolingCheckBox.Checked = false;
            componentsOptions.CaseCoolerInSetCheckBox.Checked = false;
            componentsOptions.CaseSoundIsolationCheckBox.Checked = false;
            componentsOptions.CaseDustFiltersCheckBox.Checked = false;
            componentsOptions.CasesGamingCheckBox.Checked = false;
        }

        public static void ClearGPUTextBoxes(ComponentsOptionsForm componentsOptions)
        {
            TextBox[] textBoxes =
            {
                componentsOptions.GPUIDTextBox, componentsOptions.GPUNameTextBox, componentsOptions.GPUManufactureTextBox,
                componentsOptions.GPUCapacityTextBox,componentsOptions.GPUBusWidthTextBox, componentsOptions.GPUHeightTextBox,
                componentsOptions.GPUEnergyConsumptTextBox, componentsOptions.GPUDirectXVersionTextBox,
                componentsOptions.GPUOutputInterfacesTextBox, componentsOptions.GPUCoolersCountTextBox,
                componentsOptions.GPUCoolingSysThicknessTextBox, componentsOptions.GPULengthTextBox
            };
            ComboBox[] comboboxes =
            {
                componentsOptions.GPUInterfacesComboBox,
                componentsOptions.GPUMemoryTypeComboBox,
                componentsOptions.GPUPowerTypeComboBox
            };
            componentsOptions.GPUOverclockingCheckBox.Checked = false;
            componentsOptions.GPUSLISupportCheckBox.Checked = false;
            Clear(textBoxes, comboboxes);
        }

        public static void ClearCPUTextBoxes(ComponentsOptionsForm componentsOptions)
        {
            TextBox[] textBoxes =
            {
                componentsOptions.CPUIDTextBox, componentsOptions.CPUNameTextBox, componentsOptions.CPUCoresTextBox,
                componentsOptions.CPUBaseStateTextBox, componentsOptions.CPUMaxStateTextBox,
                componentsOptions.CPUTDPTextBox, componentsOptions.CPUTechprocessTextBox
            };
            ComboBox[] comboBoxes =
            {
                componentsOptions.CPUSeriesComboBox, componentsOptions.DeliveryTypeComboBox,
                componentsOptions.CPUCodeNameComboBox, componentsOptions.CPUSocketComboBox,
                componentsOptions.CPUMemoryTypeComboBox, componentsOptions.CPUChanelsComboBox,
                componentsOptions.CPURamFrequaencyComboBox
            };
            componentsOptions.MultithreadingCheckBox.Checked = false;
            componentsOptions.CPUIntegratedGraphicCheckBox.Checked = false;
            Clear(textBoxes, comboBoxes);
        }
        private static void Clear(TextBox[] _textBoxes, ComboBox[] _comboBoxes, CheckBox[] _checkBoxes)
        {
            foreach (var i in _textBoxes) i.Clear();
            foreach (var i in _comboBoxes) i.SelectedItem = null;
            foreach (var i in _checkBoxes) i.Checked = false;
        }
        private static void Clear(TextBox[] _textBoxes, ComboBox[] _comboBoxes)
        {
            foreach (var i in _textBoxes) i.Clear();
            foreach (var i in _comboBoxes) i.SelectedItem = null;
        }
        
        private static void Clear(TextBox[] _textBoxes)
        {
            foreach (var i in _textBoxes) i.Clear();
        }



        //Раздел проверки текстовых полей на нулевые значения
        public static bool CheckNullForCoolingSystemTextBoxes(ComponentsOptionsForm componentsOptions)
        {
            TextBox[] textBoxes = {
                componentsOptions.CoolingSystemIDTextBox, componentsOptions.CoolingSystemNameTextBox,
                componentsOptions.CoolingSystemSocketsTextBox, componentsOptions.CoolingSystemCountOfHeatPipesTextBox,
                componentsOptions.CoolingSystemTypeOfBearing, componentsOptions.CoolingSystemNoiseLevelTextBox,
                componentsOptions.CoolingSystemMaxSpeedTextBox, componentsOptions.CoolingSystemConsumptionTextBox,
                componentsOptions.CoolingSystemDiametrTextBox
            };
            ComboBox[] comboBoxes = { componentsOptions.CoolingSystemPowerTypeComboBox };
            return CheckNullTextBoxes(textBoxes, comboBoxes);
        }

        public static bool CheckNullForRAMTextBoxes(ComponentsOptionsForm componentsOptions)
        {
            TextBox[] textBoxes = { componentsOptions.RAMIDTextBox, componentsOptions.RAMNameTextBox,
                componentsOptions.RAMKitTextBox, componentsOptions.RAMVoltageTextBox,
                componentsOptions.RAMCapacityTextBox, componentsOptions.RAMTimingsTextBox
            };
            ComboBox[] comboBoxes = { componentsOptions.RAMTypeComboBox, componentsOptions.RAMFrequencyComboBox };
            return CheckNullTextBoxes(textBoxes, comboBoxes);
        }

        public static bool CheckNullForCPUTextBoxes(ComponentsOptionsForm componentsOptions)
        {
            TextBox[] textBoxes =
            {
                componentsOptions.CPUIDTextBox, componentsOptions.CPUNameTextBox, componentsOptions.CPUCoresTextBox,
                componentsOptions.CPUBaseStateTextBox, componentsOptions.CPUMaxStateTextBox,
                componentsOptions.CPUTDPTextBox, componentsOptions.CPUTechprocessTextBox
            };
            ComboBox[] comboBoxes =
            {
                componentsOptions.CPUSeriesComboBox, componentsOptions.DeliveryTypeComboBox,
                componentsOptions.CPUCodeNameComboBox, componentsOptions.CPUSocketComboBox,
                componentsOptions.CPUMemoryTypeComboBox, componentsOptions.CPUChanelsComboBox,
                componentsOptions.CPURamFrequaencyComboBox
            };

            return CheckNullTextBoxes(textBoxes, comboBoxes);
        }
        public static bool CheckNullForMotherboardTextBoxes(ComponentsOptionsForm componentsOptions)
        {
            TextBox[] textBoxes = { componentsOptions.MotherboardIDTextBox, componentsOptions.MotherboardNameTextBox,
                componentsOptions.MotherboardConnectorsTextBox, componentsOptions.MotherboardSupportedCPUTextBox,
                componentsOptions.MotherboardRAMCapacityTextBox,componentsOptions.MotherboardCountOfRAMSlotsTextBox,
                componentsOptions.MotherboardExpansionsSlotsTextBox, componentsOptions.MotherboardStorageInterfacesTextBox,
                componentsOptions.MotherboardLengthTextBox, componentsOptions.MotherboardWidthTextBox
            };
            ComboBox[] comboBoxes = { componentsOptions.MotherboardSocketComboBox,componentsOptions.MotherboardRamFrequencyComboBox,
            componentsOptions.MotherboardChipsetComboBox,componentsOptions.MotherBoardRAMChanelsComboBox,
            componentsOptions.MotherboardFormFactorComboBox, componentsOptions.MotherboardSupportedRAMComboBox};

            return CheckNullTextBoxes(textBoxes, comboBoxes);
        }

        public static bool CheckNullForGPUTextBoxes(ComponentsOptionsForm componentsOptions)
        {
            TextBox[] textboxes =
            {
                componentsOptions.GPUIDTextBox, componentsOptions.GPUNameTextBox, componentsOptions.GPUManufactureTextBox,
                componentsOptions.GPUCapacityTextBox,componentsOptions.GPUBusWidthTextBox, componentsOptions.GPUHeightTextBox,
                componentsOptions.GPUEnergyConsumptTextBox, componentsOptions.GPUDirectXVersionTextBox,
                componentsOptions.GPUOutputInterfacesTextBox, componentsOptions.GPUCoolersCountTextBox,
                componentsOptions.GPUCoolingSysThicknessTextBox, componentsOptions.GPULengthTextBox
            };

            ComboBox[] comboboxes =
            {
                componentsOptions.GPUInterfacesComboBox,
                componentsOptions.GPUMemoryTypeComboBox,
                componentsOptions.GPUPowerTypeComboBox
            };
            return CheckNullTextBoxes(textboxes, comboboxes);

        }

        public static bool CheckNullForCaseTextBoxes(ComponentsOptionsForm componentsOptions)
        {
            TextBox[] textBoxes =
                        {
                componentsOptions.CaseIDTextBox,componentsOptions.CaseNameTextBox, componentsOptions.CasePSUTextBox,
                componentsOptions.CasePSUPositionTextBox, componentsOptions.CaseMaterialTextBox,
                componentsOptions.CaseSupportedMotherboardsTextBox, componentsOptions.CaseCoolingTypeTextBox,
                componentsOptions.CaseCoolersCountTextBox, componentsOptions.CaseCoolerSlotsCountTextBox,
                componentsOptions.CaseHeightTextBox, componentsOptions.CaseWidthTextBox, componentsOptions.CaseDepthTextBox,
                componentsOptions.CaseStorageLocationCountTextBox, componentsOptions.CaseExpansionSlotsCount,
                componentsOptions.CaseMaxLengthOfGPUTextBox, componentsOptions.CaseMaxCPUCoolerHeightTextBox,
                componentsOptions.CaseMaxPSULengthTextBox, componentsOptions.CaseWeightTextBox
            };
            if (!CheckNullTextBoxes(textBoxes))
            {
                if (componentsOptions.CaseFormFactorComboBox.Text == "")
                    return true;
                else
                    return false;
            }
            else
                return true;
        }

        private static bool CheckNullTextBoxes(TextBox[] _textBoxes, ComboBox[] _comboBoxes)
        {
            foreach (var i in _textBoxes)
            {
                if (i.TextLength == 0)
                    return true;
            }
            foreach (var i in _comboBoxes)
            {
                if (i.Text == "")
                    return true;
            }
            return false;
        }
        private static bool CheckNullTextBoxes(TextBox[] _textBoxes)
        {
            foreach (var i in _textBoxes)
            {
                if (i.TextLength == 0)
                    return true;
            }
            return false;
        }



        //Раздел блокировки элементов управления
        public static void ChangeCoolingSystemTextBoxesEnable(ComponentsOptionsForm componentsOptions, bool _status)
        {
            TextBox[] textBoxes = {
                componentsOptions.CoolingSystemIDTextBox, componentsOptions.CoolingSystemNameTextBox,
                componentsOptions.CoolingSystemSocketsTextBox, componentsOptions.CoolingSystemCountOfHeatPipesTextBox,
                componentsOptions.CoolingSystemTypeOfBearing, componentsOptions.CoolingSystemNoiseLevelTextBox,
                componentsOptions.CoolingSystemMaxSpeedTextBox, componentsOptions.CoolingSystemConsumptionTextBox,
                componentsOptions.CoolingSystemDiametrTextBox
            };
            ComboBox[] comboBoxes = { componentsOptions.CoolingSystemPowerTypeComboBox };
            CheckBox[] checkBoxes = {
                componentsOptions.CoolingSystemEvaporationChamberCheckBox, componentsOptions.CoolingSystemRSCCheckBox};
            SetEnableBoxes(textBoxes, comboBoxes, checkBoxes, _status);
        }

        public static void ChangeRAMTextBoxesEnable(ComponentsOptionsForm componentsOptions, bool _status)
        {
            TextBox[] textBoxes = { componentsOptions.RAMIDTextBox, componentsOptions.RAMNameTextBox,
                componentsOptions.RAMKitTextBox, componentsOptions.RAMVoltageTextBox,
                componentsOptions.RAMCapacityTextBox, componentsOptions.RAMTimingsTextBox
            };
            ComboBox[] comboBoxes = { componentsOptions.RAMTypeComboBox, componentsOptions.RAMFrequencyComboBox };
            CheckBox[] checkBoxes = { componentsOptions.RAMLowProfileModuleCheckBox, componentsOptions.RAMCoolingCheckBox,
                componentsOptions.RAMxmpSupportCheckBox
            };
            SetEnableBoxes(textBoxes, comboBoxes, checkBoxes, _status);
        }

        public static void ChangeCPUTextBoxesEnable(ComponentsOptionsForm componentsOptions, bool _status)
        {
            TextBox[] textBoxes =
            {
                componentsOptions.CPUIDTextBox, componentsOptions.CPUNameTextBox, componentsOptions.CPUCoresTextBox,
                componentsOptions.CPUBaseStateTextBox, componentsOptions.CPUMaxStateTextBox, 
                componentsOptions.CPUTDPTextBox, componentsOptions.CPUTechprocessTextBox
            };
            ComboBox[] comboBoxes =
            {
                componentsOptions.CPUSeriesComboBox, componentsOptions.DeliveryTypeComboBox,
                componentsOptions.CPUCodeNameComboBox, componentsOptions.CPUSocketComboBox,
                componentsOptions.CPUMemoryTypeComboBox, componentsOptions.CPUChanelsComboBox,
                componentsOptions.CPURamFrequaencyComboBox
            };


            componentsOptions.MultithreadingCheckBox.Enabled = _status;
            componentsOptions.CPUIntegratedGraphicCheckBox.Enabled = _status;
            SetEnableBoxes(textBoxes, comboBoxes, _status);
        }
        public static void ChangeGPUTextBoxesEnable(ComponentsOptionsForm componentsOptions,  bool _status)
        {
            TextBox[] textBoxes =
            {
                componentsOptions.GPUIDTextBox, componentsOptions.GPUNameTextBox, componentsOptions.GPUManufactureTextBox,
                componentsOptions.GPUCapacityTextBox,componentsOptions.GPUBusWidthTextBox, componentsOptions.GPUHeightTextBox,
                componentsOptions.GPUEnergyConsumptTextBox, componentsOptions.GPUDirectXVersionTextBox,
                componentsOptions.GPUOutputInterfacesTextBox, componentsOptions.GPUCoolersCountTextBox,
                componentsOptions.GPUCoolingSysThicknessTextBox, componentsOptions.GPULengthTextBox
            };
            ComboBox[] comboboxes =
            {
                componentsOptions.GPUInterfacesComboBox,
                componentsOptions.GPUMemoryTypeComboBox,
                componentsOptions.GPUPowerTypeComboBox
            };
            componentsOptions.GPUOverclockingCheckBox.Enabled = _status;
            componentsOptions.GPUSLISupportCheckBox.Enabled = _status;
            SetEnableBoxes(textBoxes, comboboxes, _status);
        }

        public static void ChangeMotherboardTextBoxesEnable(ComponentsOptionsForm componentsOptions, bool _status)
        {
            TextBox[] textBoxes = { componentsOptions.MotherboardIDTextBox, componentsOptions.MotherboardNameTextBox,
                componentsOptions.MotherboardConnectorsTextBox, componentsOptions.MotherboardSupportedCPUTextBox, 
                componentsOptions.MotherboardRAMCapacityTextBox,componentsOptions.MotherboardCountOfRAMSlotsTextBox, 
                componentsOptions.MotherboardExpansionsSlotsTextBox, componentsOptions.MotherboardStorageInterfacesTextBox, 
                componentsOptions.MotherboardLengthTextBox, componentsOptions.MotherboardWidthTextBox
            };
            ComboBox[] comboBoxes = { componentsOptions.MotherboardSocketComboBox,componentsOptions.MotherboardRamFrequencyComboBox,
            componentsOptions.MotherboardChipsetComboBox,componentsOptions.MotherBoardRAMChanelsComboBox,
            componentsOptions.MotherboardFormFactorComboBox, componentsOptions.MotherboardSupportedRAMComboBox};
            componentsOptions.MotherboardIntegratedGraphicCheckBox.Enabled = _status;
            componentsOptions.MotherboardSLISupportCheckBox.Enabled = _status;
            SetEnableBoxes(textBoxes, comboBoxes, _status);
        }

        public static void ChangeCaseTextBoxesEnable(ComponentsOptionsForm componentsOptions, bool _status)
        {
            TextBox[] textBoxes =
           {
                componentsOptions.CaseIDTextBox,componentsOptions.CaseNameTextBox, componentsOptions.CasePSUTextBox,
                componentsOptions.CasePSUPositionTextBox, componentsOptions.CaseMaterialTextBox,
                componentsOptions.CaseSupportedMotherboardsTextBox, componentsOptions.CaseCoolingTypeTextBox,
                componentsOptions.CaseCoolersCountTextBox, componentsOptions.CaseCoolerSlotsCountTextBox,
                componentsOptions.CaseHeightTextBox, componentsOptions.CaseWidthTextBox, componentsOptions.CaseDepthTextBox,
                componentsOptions.CaseStorageLocationCountTextBox, componentsOptions.CaseExpansionSlotsCount,
                componentsOptions.CaseMaxLengthOfGPUTextBox, componentsOptions.CaseMaxCPUCoolerHeightTextBox,
                componentsOptions.CaseMaxPSULengthTextBox, componentsOptions.CaseWeightTextBox
            };
            componentsOptions.CaseFormFactorComboBox.Enabled = _status;
            SetEnableBoxes(textBoxes, _status);
            componentsOptions.CaseWaterCoolingCheckBox.Enabled = _status;
            componentsOptions.CaseCoolerInSetCheckBox.Enabled = _status;
            componentsOptions.CaseSoundIsolationCheckBox.Enabled = _status;
            componentsOptions.CaseDustFiltersCheckBox.Enabled = _status;
            componentsOptions.CasesGamingCheckBox.Enabled = _status;
        }

        private static void SetEnableBoxes(TextBox[] _textBoxes, ComboBox[] _comboBoxes, CheckBox[] _checkBoxes, bool _status)
        {
            
            foreach (var i in _textBoxes)
            {
                i.Enabled = _status;
            }
            foreach (var i in _comboBoxes)
            {
                i.Enabled = _status;
            }
            foreach (var i in _checkBoxes)
            {
                i.Enabled = _status;
            }
        }
        private static void SetEnableBoxes(TextBox[] _textBoxes, ComboBox[] _comboBoxes, bool _status)
        {
            foreach (var i in _textBoxes)
            {
                i.Enabled = _status;
            }
            foreach (var i in _comboBoxes)
            {
                i.Enabled = _status;
            }
        }
        private static void SetEnableBoxes(TextBox[] _textBoxes, bool _status)
        {
            foreach (var i in _textBoxes)
            {
                i.Enabled = _status;
            }
        }



        //Раздел проверки на возможность преобразования к числовому типу
        public static bool CheckNumConvertForCoolingSystemTextBoxes(ComponentsOptionsForm componentsOptions)
        {
            TextBox[] textBoxes = {
                componentsOptions.CoolingSystemCountOfHeatPipesTextBox, componentsOptions.CoolingSystemMaxSpeedTextBox, 
                componentsOptions.CoolingSystemConsumptionTextBox, componentsOptions.CoolingSystemDiametrTextBox
            };
            float res;
            bool isFloat = float.TryParse(componentsOptions.CoolingSystemNoiseLevelTextBox.Text, out res);
            //true - если распарсит
            if (!isFloat)
                return false;
            else
                return CheckIntConvert(textBoxes);
        }


        public static bool CheckNumConvertForRAMTextBoxes(ComponentsOptionsForm componentsOptions)
        {
            TextBox[] textBoxes = {componentsOptions.RAMKitTextBox, componentsOptions.RAMCapacityTextBox};
            //componentsOptions.RAMVoltageTextBox - float
            float res;
            bool isFloat = float.TryParse(componentsOptions.RAMVoltageTextBox.Text, out res);
            //true - если распарсит
            if (!isFloat)
                return false;
            else
                return CheckIntConvert(textBoxes);
        }

        public static bool CheckNumConvertForCPUTextBoxes(ComponentsOptionsForm componentsOptions)
        {
            TextBox[] textBoxes =
            {
                componentsOptions.CPUCoresTextBox, componentsOptions.CPUBaseStateTextBox,
                componentsOptions.CPUMaxStateTextBox, componentsOptions.CPUTDPTextBox,
                componentsOptions.CPUTechprocessTextBox
            };
            return CheckIntConvert(textBoxes);
        }

        public static bool CheckNumConvertForCaseTextBoxes(ComponentsOptionsForm componentsOptions)
        {
            TextBox[] textBoxes = {
                componentsOptions.CaseCoolersCountTextBox, componentsOptions.CaseCoolerSlotsCountTextBox,
                componentsOptions.CaseHeightTextBox, componentsOptions.CaseWidthTextBox, componentsOptions.CaseDepthTextBox,
                componentsOptions.CaseStorageLocationCountTextBox, componentsOptions.CaseExpansionSlotsCount,
                componentsOptions.CaseMaxLengthOfGPUTextBox, componentsOptions.CaseMaxCPUCoolerHeightTextBox,
                componentsOptions.CaseMaxPSULengthTextBox, 

            };
            float res;
            bool isFloat = float.TryParse(componentsOptions.CaseWeightTextBox.Text, out res);
            //true - если распарсит
            if (!isFloat)
                return false;
            else
                return CheckIntConvert(textBoxes);
        }
        public static bool CheckNumConvertGPUTextBoxes(ComponentsOptionsForm componentsOptions)
        {
            TextBox[] textBoxes =
            {
                componentsOptions.GPUBusWidthTextBox, componentsOptions.GPUCoolersCountTextBox,
                componentsOptions.GPUCoolingSysThicknessTextBox, componentsOptions.GPUCapacityTextBox,
                componentsOptions.GPUEnergyConsumptTextBox, componentsOptions.GPULengthTextBox,
                componentsOptions.GPUHeightTextBox
            };
            return (CheckIntConvert(textBoxes));
        }

        public static bool CheckNumConvertMotherboardTextBoxes(ComponentsOptionsForm componentsOptions)
        {
            TextBox[] textBoxes = { 
                componentsOptions.MotherboardRAMCapacityTextBox, componentsOptions.MotherboardCountOfRAMSlotsTextBox, 
                componentsOptions.MotherboardLengthTextBox, componentsOptions.MotherboardWidthTextBox
            };
            return (CheckIntConvert(textBoxes));
        }

        private static bool CheckIntConvert(TextBox [] _textBoxes)
        {
            foreach (var i in _textBoxes)
            {
                int res;
                bool isInt = Int32.TryParse(i.Text, out res);
                //true - если распарсит
                if (!isInt)
                    return false;
            }
            return true;
        }





        //Раздел для разного
        public static void SetEditOrAddButtonMode(Button _button, bool _workMode)
        {
            //true - добавление
            //false - изменение
            if (_workMode)
            {
                _button.Text = "Добавить";
                _button.Enabled = true;
                _button.BackColor = Color.Green;
            }
            else
            {
                _button.Enabled = true;
                _button.Text = "Изменить";
                _button.BackColor = Color.BlueViolet;
            }
        }

        public static void SetVisibleLables(ComponentsOptionsForm componentsOptions,bool _state)
        {
            componentsOptions.label4.Visible = _state;
            componentsOptions.ComponentTypeComboBox.Visible = _state;
        }

        public static void SetButtonsDefaultOptions(params Button[] _buttons)
        {
            foreach (var i in _buttons)
            {
                i.Enabled = false;
                i.Text = "Действие не выбрано";
                i.BackColor = Color.Transparent;
            }
        }

        public static void LockButtonInSecondTab(ComponentsOptionsForm componentsOptions)
        {
            if ((componentsOptions.ComponentNameTextBox.Text == "") || 
                ((componentsOptions.AddNewComponent.Checked == false) &&
                (componentsOptions.EditComponent.Checked == false)))
                componentsOptions.ActToComponent.Enabled = false;
            else
                componentsOptions.ActToComponent.Enabled = true;
        }

    }
}