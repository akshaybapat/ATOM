
jQuery.validator.unobtrusive.adapters.add("dropdown", function (options) {
    if (options.element.tagName.toUpperCase() == "SELECT" && options.element.type.toUpperCase() == "SELECT-ONE") {
        options.rules["required"] = true;
        if (options.message) {
            options.messages["required"] = options.message;
        }
    }
});

$(function () {

    $('#CountriesDivID').hide();
    $('#FacilitiesDivID').hide();
    $('#NextButton').hide();

       $('#RegionID').change(function () {
        
        $('#CountriesDivID').hide();
        $('#FacilitiesDivID').hide();

           //if ($('#RegionID')[0].selectedIndex <= 0 || $('#CountriesID')[0].selectedIndex <= 0 || $('#FacilitiesID')[0].selectedIndex <= 0)
        if ($('#RegionID').selectedIndex <= 0 || $('#RegionID').is(":hidden") || $('#CountriesID').selectedIndex <= 0 || $('#CountriesID').is(":hidden") || $('#FacilitiesID').selectedIndex <= 0 || $('#FacilitiesID').is(":hidden"))
            $('#NextButton').hide();
        

        $.getJSON('/DimCountry/CountryList', { id: $('#RegionID').val() }, function (data) {
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

        if ($('#RegionID').selectedIndex <= 0 || $('#RegionID').is(":hidden") || $('#CountriesID').selectedIndex <= 0 || $('#CountriesID').is(":hidden") || $('#FacilitiesID').selectedIndex <= 0 || $('#FacilitiesID').is(":hidden"))
            $('#NextButton').hide();
       

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

    $('#FacilitiesID').change(function () {

        //if ($('#RegionID').selectedIndex <= 0 || $('#CountriesID').selectedIndex <= 0 || $('#FacilitiesID').selectedIndex <= 0)
        if ($('#RegionID').selectedIndex <= 0 || $('#RegionID').is(":hidden") || $('#CountriesID').selectedIndex <= 0 || $('#CountriesID').is(":hidden") || $('#FacilitiesID').selectedIndex <= 0 || $('#FacilitiesID').is(":hidden"))
            $('#NextButton').hide();
        else
            $('#NextButton').show();

            $('#NextButton').disabled=true;
     });
});

