
jQuery.validator.unobtrusive.adapters.add("dropdown", function (options) {
    if (options.element.tagName.toUpperCase() == "SELECT" && options.element.type.toUpperCase() == "SELECT-ONE") {
        options.rules["required"] = true;
        if (options.message) {
            options.messages["required"] = options.message;
        }
    }
});

function siteSelectionMenu() {

    $('#CountriesDivID').hide();
    $('#FacilitiesDivID').hide();
    $('#NextButton').hide();


    $('#RegionID').load(function () {

        $.getJSON('/FFSite/GetDropDownData', { typeofData: "RegionList" }, function (data) {
            var items = '<option>Select a Country</option>';
            $.each(data, function (i, region) {
                items += "<option value='" + region.Value + "'>" + region.Text + "</option>";
            });
            $('#RegionID').html(items);
            $('#RegionID').show();

        });

    })

    $('#RegionID').change(function () {

        $('#CountriesDivID').hide();
        $('#FacilitiesDivID').hide();

        if ($('#RegionID').selectedIndex <= 0 || $('#RegionID').is(":hidden") || $('#CountriesID').selectedIndex <= 0 || $('#CountriesID').is(":hidden") || $('#FacilitiesID').selectedIndex <= 0 || $('#FacilitiesID').is(":hidden"))
            $('#NextButton').hide();


        $.getJSON('/FFSite/GetDropDownData', { typeofData: "CountryList", filter: $('#RegionID').val() }, function (data) {
            var items = '<option>Select a Country</option>';
            $.each(data, function (i, country) {
                items += "<option value='" + country.id + "'>" + country.CountryName + "</option>";
            });
            $('#CountriesID').html(items);
            $('#CountriesDivID').show();

        });
    });

    $('#CountriesID').change(function () {

        $('#FacilitiesDivID').hide();

        if ($('#RegionID').selectedIndex <= 0 || $('#RegionID').is(":hidden") || $('#CountriesID').selectedIndex <= 0 || $('#CountriesID').is(":hidden") || $('#FacilitiesID').selectedIndex <= 0 || $('#FacilitiesID').is(":hidden"))
            $('#NextButton').hide();


        $.getJSON('/FFSite/GetDropDownData', { typeofData: "FacilityList", filter: $('#CountriesID option:selected').text() }, function (data) {
            var items = '<option>Select a Facility</option>';
            $.each(data, function (i, facility) {
                items += "<option value='" + facility.id + "'>" + facility.SiteName + "</option>";
            });
            $('#FacilitiesID').html(items);
            $('#FacilitiesDivID').show();

        });
    });

    $('#FacilitiesID').change(function () {

        if ($('#RegionID').selectedIndex <= 0 || $('#RegionID').is(":hidden") || $('#CountriesID').selectedIndex <= 0 || $('#CountriesID').is(":hidden") || $('#FacilitiesID').selectedIndex <= 0 || $('#FacilitiesID').is(":hidden"))
            $('#NextButton').hide();
        else
            $('#NextButton').show();
      
    });


}

function buildingSelectionMenu() {

    $('#NextButton').on('click',function(){

        $.getJSON('/FFSite/GetDropDownData', { typeofData: "BuildingList", filter: $('#FacilitiesID option:selected').text() }, function (data) {
            var items = '<option>Select a Building</option>';
            var result = '';
            $.each(data, function (i, building) {
                items += "<option value='" + building.id + "'>" + building.BuildingName + "</option>";
                result += '<tr style="width:50%;"><td style="border-radius:15px;">' + building.BuildingName + '</td><td id="selectedID" style="display:none;">' + building.id + '</tr>';
            });
            $('#BuildingsID').html(items);
            $('#resultset').html(result);
            //});


            /* Get all rows from your 'table' but not the first one 
            * that includes headers. */
            var row = '';
            var selectedID = '';
            var rows = $('#resultset tr');
            var highligthed = 'false';

            /* Create 'click' event handler for rows */
            rows.on('click', function (e) {

                /* Get current row */
                row = $(this);
                /* Get index of column for Building id*/
                var column = $('#selectedID').index();
                /* Get value of Building id of the current row*/
                selectedID = row.find('td').eq(column).html();

                if (highligthed == false)
                    /* Highlight one row and clean others */ {
                    rows.removeClass('highlight');
                    row.addClass('highlight');
                    highligthed = true;
                }
                else {
                    rows.removeClass('highlight');
                    highligthed = false;
                }

            });

            /* This 'event' is used just to avoid that the table text 
            * gets selected (just for styling). 
            * For example, when pressing 'Shift' keyboard key and clicking 
            * (without this 'event') the text of the 'table' will be selected.
            * You can remove it if you want, I just tested this in 
            * Chrome v30.0.1599.69 */
            $(document).bind('selectstart dragstart', function (e) {
                e.preventDefault(); return false;
            });


            $('#EditButton').click(function () {
                var url = '/DimBuildings/Edit?id=' + selectedID;
                window.location.href = url;
            });

            $('#DeleteButton').click(function () {
                var url = '/DimBuildings/Delete?id=' + selectedID;
                window.location.href = url;
            });


        });

    });

}

function moduleSelectionMenu() {



}