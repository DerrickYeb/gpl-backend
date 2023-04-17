 public IList<SybrinChequeDTO> ReadExcelFile()
        {
            try
            {
                logger.Info("Reading Excel file");
              
                string creationDate = DateTime.Now.Date.ToString("yyyyMMdd");
                string creationTime = DateTime.Now.ToString("HHmmss");
                //string fileName = $"\\CorporateCheques_{creationDate}_{creationTime}.xlsx";
                List<SybrinChequeDTO> list = new List<SybrinChequeDTO>();
                DataSet chequeInfos = new DataSet();
                DataTable dt = new DataTable();

                foreach (var item in Directory.GetFiles(SybrinExcelLocalFilePath,"*.xlsx"))
                {
                    //FileInfo file = new FileInfo(item);

                    using (ExcelPackage excel = new ExcelPackage(item,""))
                    {
                        
                        ExcelWorksheet excelWorksheet = excel.Workbook.Worksheets[0];

                        //Removiing Read-Only protection

                        if (dt.Columns.Count == 0)
                        {
                            foreach (var column in excelWorksheet.Columns)
                            {
                                DataColumn col = new DataColumn($"{column.Range.Text}");
                                dt.Columns.Add(col);
                            }
                            //for (int col = 0; col <= 15; col++)
                            //{
                            //    DataColumn column = new DataColumn($"Column{col}");
                            //    dt.Columns.Add(column);
                            //}
                        }
                        //excelWorksheet.Workbook.Protection.WriteProtection.RemoveReadOnly();

                        int rowCount = excelWorksheet.Dimension.End.Row;
                        logger.Info("Reading Excel File from File");
                        //Logger.LogInfo("Reading Excel File");
                        for (int row = 1; row < rowCount; row++)
                        {
                            row += 1;
                            DataRow dataRow = dt.NewRow();
                            dataRow[0] = excelWorksheet.Cells[row, 1].Value.ToString();
                            dataRow[1] = excelWorksheet.Cells[row, 2].Value.ToString();
                            dataRow[2] = excelWorksheet.Cells[row, 3].Value.ToString();
                            dataRow[3] = excelWorksheet.Cells[row, 4].Value.ToString();
                            dataRow[4] = excelWorksheet.Cells[row, 5].Value.ToString();
                            dataRow[5] = excelWorksheet.Cells[row, 6].Value.ToString();
                            dataRow[6] = excelWorksheet.Cells[row, 7].Value.ToString();
                            dataRow[7] = excelWorksheet.Cells[row, 8].Value.ToString();
                            dataRow[8] = excelWorksheet.Cells[row, 9].Value.ToString();
                            dataRow[9] = excelWorksheet.Cells[row, 10].Value.ToString();
                            dataRow[10] = excelWorksheet.Cells[row, 11].Value.ToString();
                            dataRow[11] = excelWorksheet.Cells[row, 12].Value.ToString();
                            dataRow[12] = excelWorksheet.Cells[row, 13].Value.ToString();
                            dataRow[13] = excelWorksheet.Cells[row, 14].Value.ToString();
                            dataRow[14] = excelWorksheet.Cells[row, 15].Value.ToString();
                            dataRow[15] = excelWorksheet.Cells[row, 16].Value.ToString();
                            dt.Rows.Add(dataRow);
                           
                            logger.Info($"Data Extracted - {list}");
                        }
                    }

                }
                chequeInfos.Tables.Add(dt);
                var chequesList = chequeInfos.Tables[0].AsEnumerable().Select(x => new SybrinChequeDTO
                {
                    Amount = Convert.ToString(x["Amount"] != DBNull.Value ? x["Amount"] : ""),
                    BackImageName = Convert.ToString(x["Back Image name"] != DBNull.Value ? x["Back Image name"] : ""),
                    ChequeAccountNumber = Convert.ToString(x["Cheque Account Number"] != DBNull.Value ? x["Cheque Account Number"] : ""),
                    DepositorAccountNumber = Convert.ToString(x["Depositor Account Number"] != DBNull.Value ? x["Depositor Account Number"] : ""),
                    FrontImageName = Convert.ToString(x["Front Image name"] != DBNull.Value ? x["Front Image name"] : ""),
                    ItemID = Convert.ToString(x["Item ID"] != DBNull.Value ? x["Item ID"] : ""),
                    ItemSequenceNo = Convert.ToString(x["EJ Item Sequence No"] != DBNull.Value ? x["EJ Item Sequence No"] : ""),
                    PaymentStatus = Convert.ToString(x["Pay/Unpay"] != DBNull.Value ? x["Pay/Unpay"] : ""),
                    PresentingBank = Convert.ToString(x["Presenting Bank"] != DBNull.Value ? x["Presenting Bank"] : ""),
                    SerialNumber = Convert.ToString(x["Serial Number"] != DBNull.Value ? x["Serial Number"] : ""),
                    Session = Convert.ToString(x["Session"] != DBNull.Value ? x["Session"] : ""),
                    SortCode = Convert.ToString(x["Sort Code"] != DBNull.Value ? x["Sort Code"] : ""),
                    VirtualSortCode = Convert.ToString(x["Virtual Sort Code"] != DBNull.Value ? x["Virtual Sort Code"] : ""),
                    EJAccountNumber = Convert.ToString(x["EJ Account Number"] != DBNull.Value ? x["EJ Account Number"] : ""),
                    TransactionDate = Convert.ToString(x["Transaction Date/Presentment Date"] != DBNull.Value ? x["Transaction Date/Presentment Date"] : ""),
                    UnpayReason = Convert.ToString(x["Unpay Reason"] != DBNull.Value ? x["Unpay Reason"] : "")
                }).ToList();


                return chequesList;
            }
            catch (Exception e)
            {
                //Logger.LogError(e.Message);
                logger.Error(e.Message);
                throw;
            }
        }