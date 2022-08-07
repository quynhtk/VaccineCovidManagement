
$(function () {
    var l = abp.localization.getResource('VaccineCovidManagement');

    var datatable = $('#VaccineTonKhoTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: true,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(vaccineCovidManagement.vaccineTonKhos.vaccineTonKho.getList),
            columnDefs: [
                {
                    title: l("STT"),
                    data: 'stt'
                },
                {
                    title: l("Tên Vaccine"),
                    data: "tenVaccineTonKho"
                },
                {
                    title: l("Số lượng tồn kho"),
                    data: "soLuongTonKho"
                },
                {
                    title: l('Thời gian tạo'), data: "creationTime",
                    render: function (data) {
                        return luxon
                            .DateTime
                            .fromISO(data, {
                                locale: abp.localization.currentCulture.name
                            }).toLocaleString(luxon.DateTime.DATETIME_SHORT);
                    }
                }
            ]
        })
    );
});