$(function () {

    $('#CountriesDivID').hide();
    $('#FacilitiesDivID').hide();
   
    $('#RegionID').change(function () {
        
        $('#CountriesDivID').hide();
        $('#FacilitiesDivID').hide();

        $.getJSON('/DimFacility/CountryList', { id: $('#RegionID').val() }, function (data) {
            var items = '<option>Select a Country</option>';
            $.each(data, function (i, country) {
                items += "<option value='" + country.Value + "'>" + country.Text + "</option>";
                // state.Value cannot contain ' character. We are OK because state.Value = cnt++;
            });
            $('#CountriesID').html(items);
            $('#CountriesDivID').show();

        });
    });

    $('#CountriesID').change(function () {
        
        $('#FacilitiesDivID').hide();

        $.getJSON('/DimFacility/FacilityList', { id: $('#CountriesID option:selected').text() }, function (data) {
            var items = '<option>Select a Facility</option>';
            $.each(data, function (i, facility) {
                items += "<option value='" + facility.Value + "'>" + facility.Text + "</option>";
                // state.Value cannot contain ' character. We are OK because state.Value = cnt++;
            });
            $('#FacilitiesID').html(items);
            $('#FacilitiesDivID').show();

        });
    });

});