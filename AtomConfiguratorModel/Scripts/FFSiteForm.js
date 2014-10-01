
jQuery.validator.unobtrusive.adapters.add("dropdown", function (options) {
    if (options.element.tagName.toUpperCase() == "SELECT" && options.element.type.toUpperCase() == "SELECT-ONE") {
        options.rules["required"] = true;
        if (options.message) {
            options.messages["required"] = options.message;
        }
    }
});


$.fn.scrollTo = function (target, options, callback) {
    if (typeof options == 'function' && arguments.length == 2) { callback = options; options = target; }
    var settings = $.extend({
        scrollTarget: target,
        offsetTop: 50,
        duration: 500,
        easing: 'swing'
    }, options);
    return this.each(function () {
        var scrollPane = $(this);
        var scrollTarget = (typeof settings.scrollTarget == "number") ? settings.scrollTarget : $(settings.scrollTarget);
        var scrollY = (typeof scrollTarget == "number") ? scrollTarget : scrollTarget.offset().top + scrollPane.scrollTop() - parseInt(settings.offsetTop);
        scrollPane.animate({ scrollTop: scrollY }, parseInt(settings.duration), settings.easing, function () {
            if (typeof callback == 'function') { callback.call(this); }
        });
    });
}



function gotoURL(url) {

    var xhr = new XMLHttpRequest();

    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {

            $('#bigblue').show();

            window.location.href = url;
        }


    }

    xhr.open('head', url);
    xhr.send(null);
    $('#bigblue').hide();

}

$('#rightnavigation').on('click', function (event) {
    $('#ffinstance').scrollLeft(2000)

});

$('#leftnavigation').on('click', function (event) {
    $('#ffinstance').scrollLeft(-2000)

});


$('table tr').on('click', function (e) {

    var state = $(this).hasClass('highlight');

    /*Reset field values and Next Button */
    $('.highlight').removeClass('highlight');
   
    if (!state) {

        $(this).addClass('highlight');

    }


});



$('#SearchBox').on("keyup paste", function () {

    var value = $(this).val().toUpperCase();
    var $rows = $("table tr");

    if (value === '') {
        $rows.show();
        return false;
    }

    $rows.each(function (index) {
        if (index !== 0) {

            $row = $(this);

            var column1 = $row.find("td").eq(0).text().toUpperCase();
            var column2 = $row.find("td").eq(1).text().toUpperCase();

            if ((column1.indexOf(value) > -1) || (column2.indexOf(value) > -1)) {
                $row.show();
            }
            else {
                $row.hide();
            }
        }
    });

});



    $('#CountriesDivID').hide();
    $('#FacilitiesDivID').hide();
    $('#Next-ToBuildingSelection').hide();


    $('#RegionID').load(function () {

        $.getJSON('/FFSite/GetDropDownData', { typeofData: "RegionList" }, function (data) {
            var items = '<option>Select a Country</option>';
            $.each(data, function (i, region) {
                items += "<option value='" + region.Value + "'>" + region.Text + "</option>";
            });
            $('#RegionID').html(items);
            $('#RegionID').show();

        });

    });

    $('#RegionID').change(function () {

        $('#CountriesDivID').hide();
        $('#FacilitiesDivID').hide();

        if ($('#RegionID').selectedIndex <= 0 || $('#RegionID').is(":hidden") || $('#CountriesID').selectedIndex <= 0 || $('#CountriesID').is(":hidden") || $('#FacilitiesID').selectedIndex <= 0 || $('#FacilitiesID').is(":hidden"))
            $('#Next-ToBuildingSelection').hide();


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
            $('#Next-ToBuildingSelection').hide();


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
            $('#Next-ToBuildingSelection').hide();
        else
            $('#Next-ToBuildingSelection').show();
    });


    $('#Next-ToBuildingSelection').on('click', function () {

        $('#Next-ToModuleSelection').hide();


        $.getJSON('/FFSite/GetDropDownData', { typeofData: "BuildingList", filter: $('#FacilitiesID option:selected').text() }, function (data) {
            var items = '<option>Select a Building</option>';
            var result = '';
            $.each(data, function (i, building) {
                items += "<option value='" + building.id + "'>" + building.BuildingName + "</option>";
                result += '<tr style="width:50%;"><td id="selectedBuildingName" style="border-radius:15px;font-weight: bold">' + building.BuildingName + '</td><td id="selectedBuildingID" style="display:none;">' + building.id + '</tr>';
            });

            if (result.length < 2) result = '<tr><td><h4><b>Please Select a Valid Site</b></h4></td></tr>';

            // $('#BuildingsID').html(items);
            $('#buildingsresultset').html(result);


            /* Get all rows from your 'table' but not the first one 
            * that includes headers. */
            var row = '';
            var rows = $('#buildingsresultset tr');

            /* Create 'click' event handler for rows */
            rows.on('click', function (e) {

                var state = $(this).hasClass('highlight');

                /*Reset field values and Next Button*/
                $('#buildingsresultset tr.highlight').removeClass('highlight');
                $('#Next-ToModuleSelection').hide();
                selectedBuildingID = '';

                $("#progressbar li").eq(1).text("Building Selection");


                if (!state) {

                    $(this).addClass('highlight');

                    /* Get current row */
                    row = $(this);

                    /* Get index of column for Building id*/
                    var column = $('#selectedBuildingID').index();

                    /* Get value of Building id of the current row*/
                    selectedBuildingID = row.find('td').eq(column).html();

                    $("#progressbar li").eq(1).append("<br/> { " + row.find('td').eq(0).html() + " }");

                    /*Display Next Button */
                    $('#Next-ToModuleSelection').show();
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


            $('#EditBuildingButton').click(function () {
                var url = '/DimBuildings/Edit?id=' + selectedBuildingID;
                //window.location.href = url;
                gotoURL(url);

            });

            $('#DeleteBuildingButton').click(function () {
                var url = '/DimBuildings/Delete?id=' + selectedBuildingID;
                //window.location.href = url;
                gotoURL(url);

            });


        });

    });

    $('#Next-ToModuleSelection').on('click', function () {

        $('#Next-ToFFSelection').hide();

        var selection = '';

        selection = $('#buildingsresultset tr.highlight td').filter('#selectedBuildingName').html()

        var result = '';

        $.getJSON('/FFSite/GetDropDownData', { typeofData: "ModuleList", filter: selection }, function (data) {


            var items = '<option>Select a Module</option>';

            $.each(data, function (i, module) {
                items += "<option value='" + module.id + "'>" + module.ModuleName + "</option>";
                result += '<tr style="width:50%;"><td id="selectedModuleName" style="border-radius:15px;font-weight: bold">' + module.ModuleName + '</td><td id="selectedModuleID" style="display:none;">' + module.id + '</tr>';
            });

            if (result.length < 2) result = '<tr><td><h4><b>Please Select a Valid Building</b></h4></td></tr>';

            //$('#ModulesID').html(items);
            $('#modulesresultset').html(result);


            /* Get all rows from your 'table' but not the first one 
            * that includes headers. */
            var row = '';
            var selectedModuleID = '';
            var rows = $('#modulesresultset tr');

            /* Create 'click' event handler for rows */
            rows.on('click', function (e) {

                var state = $(this).hasClass('highlight');

                /*Reset field values*/
                $('#modulesresultset tr.highlight').removeClass('highlight');
                selectedModuleID = '';

                $("#progressbar li").eq(2).text("Module Selection");

                if (!state) {

                    $(this).addClass('highlight');

                    /* Get current row */
                    row = $(this);

                    /* Get index of column for Module id*/
                    var column = $('#selectedModuleID').index();

                    /* Get value of Module id of the current row*/
                    selectedModuleID = row.find('td').eq(column).html();

                    $("#progressbar li").eq(2).append("<br/> { " + row.find('td').eq(0).html() + " }");

                    /*Display Next Button */
                    $('#Next-ToFFSelection').show();

                };
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


            $('#EditModuleButton').click(function () {
                var url = '/DimModules/Edit?id=' + selectedModuleID;
                //window.location.href = url;
                gotoURL(url);
            });

            $('#DeleteModuleButton').click(function () {
                var url = '/DimModules/Delete?id=' + selectedModuleID;
                //window.location.href = url;
                gotoURL(url);
            });


        });

    });


    $('#Next-ToFFSelection').on('click', function () {
        var selection = '';

        selection = $('#modulesresultset tr.highlight td').filter('#selectedModuleName').html()

        var result = '';

        $.getJSON('/FFSite/GetDropDownData', { typeofData: "FFInstanceList", filter: selection }, function (data) {


            var items = '<option>Select a FlexFlow Instance</option>';

            $.each(data, function (i, ffinstance) {
                items += "<option value='" + ffinstance.id + "'>" + ffinstance.ProjectName + "</option>";
                result += '<tr style="width:50%;"><td id="selectedFFInstance" style="border-radius:15px;font-weight: bold">' + ffinstance.ProjectName + '</td><td id="selectedFFInstanceID" style="display:none;">' + ffinstance.id + '</tr>';
            });

            if (result.length < 2) result = '<tr><td><h4><b>Please Select a Valid FlexFlow Instance</b></h4></td></tr>';

            //$('#ModulesID').html(items);
            $('#ffinstancesresultset').html(result);


            /* Get all rows from your 'table' but not the first one 
            * that includes headers. */
            var row = '';
            var selectedFFInstanceID = '';
            var rows = $('#ffinstancesresultset tr');

            /* Create 'click' event handler for rows */
            rows.on('click', function (e) {

                var state = $(this).hasClass('highlight');

                /*Reset field values*/
                $('#ffinstancesresultset tr.highlight').removeClass('highlight');
                selectedFFInstanceID = '';

                $("#progressbar li").eq(3).text("FlexFlow Instance Selection");

                if (!state) {

                    $(this).addClass('highlight');

                    /* Get current row */
                    row = $(this);

                    /* Get index of column for Module id*/
                    var column = $('#selectedFFInstanceID').index();

                    /* Get value of Module id of the current row*/
                    selectedFFInstanceID = row.find('td').eq(column).html();

                    $("#progressbar li").eq(3).append("<br/> { " + row.find('td').eq(0).html() + " }");

                };
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


            $('#EditFFButton').click(function () {
                var url = '/DimFFInstance/Edit?id=' + selectedFFInstanceID;
                //window.location.href = url;
                gotoURL(url);
            });

            $('#DeleteFFButton').click(function () {
                var url = '/DimFFInstance/Delete?id=' + selectedFFInstanceID;
                //window.location.href = url;
                gotoURL(url);
            });


        });

    });


    $('#Next-ToBucketSelection').on('click', function () {
        var selection = '';

        selection = $('#ffinstancesresultset tr.highlight td').filter('#selectedFFInstance').html()

        //selection = selectedFFInstanceID;

        var result = '';

        $.getJSON('/FFSite/GetDropDownData', { typeofData: "BucketsList" }, function (data) {
            var items = '<option>Select a Bucket Type</option>';
            $.each(data, function (i, bucket) {
                items += "<option value='" + bucket.id + "'>" + bucket.BucketName + "</option>";
            });
            $('#BucketsDropDown').html(items);
          

        });

        $.getJSON('/FFSite/GetDropDownData', { typeofData: "StationTypesList", filter: selection }, function (data) {


            var items = '<option>Select a Station Type</option>';

            $.each(data, function (i, stationtype) {
                items += "<option value='" + stationtype.id + "'>" + stationtype.StationTypeName + "</option>";
                //result += '<tr  class="simple_with_animation" style="width:50%;"><td id="selectedStationTypeName" style="border-radius:15px;font-weight: bold">' + stationtype.StationTypeName + '</td><td id="selectedStationTypeID" style="display:none;">' + stationtype.id + '</tr>';
                result += '<li id="selectedStationTypeName" style="border-radius:15px;">' + stationtype.StationTypeName + '<div id="selectedStationTypeID" style="display:none;">' + stationtype.id + '</div></li>';
            });

            if (result.length < 2) result = '<tr><td><h4><b>Please Select a Valid FlexFlow Instance</b></h4></td></tr>';

            $('#stationtypesresultset').html(result);
            //$('#bucketypesresultset').html(result);

            /* Get all rows from your 'table' but not the first one 
            * that includes headers. */
            var row = '';
            var selectedFFInstanceID = '';
            var rows = $('#stationtypesresultset li');

            /* Create 'click' event handler for rows */
           // rows.on('click', function (e) {
            /*Start Highlight logic

            var state = $(this).hasClass('highlight');

            /*Reset field values
            $('#stationtypesresultset li.highlight').removeClass('highlight');
            selectedFFInstanceID = '';

            $("#progressbar li").eq(4).text("Bucket Selection");

            if (!state) {

                $(this).addClass('highlight');

                /* Get current row 
                row = $(this);

                /* Get index of column for Module id
                var column = $('#selectedStationTypeID').index();

                /* Get index of column for Module id
                var station = $('#selectedStationTypeName').index();

                /* Get value of Module id of the current row
                selectedFFInstanceID = row.find('#selectedStationTypeID').html();

                $("#progressbar li").eq(4).append("<br/> { " + row.html() + " }");

            };

            //End Highlight logic 
                
           // });

            /* This 'event' is used just to avoid that the table text 
            * gets selected (just for styling). 
            * For example, when pressing 'Shift' keyboard key and clicking 
            * (without this 'event') the text of the 'table' will be selected.
            * You can remove it if you want, I just tested this in 
            * Chrome v30.0.1599.69 */
            $(document).bind('selectstart dragstart', function (e) {
                e.preventDefault(); return false;
            });


        });

    });


$("#stationtypesresultset, #bucketypesresultset").sortable({
    connectWith: ".simple_with_animation"
    }).disableSelection();