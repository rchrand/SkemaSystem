SkemaSystem
===========

A new SkeamSystem for EAAA, For a SUM Project!

# Design Guidelines
## Grid
Vigtige classes: row og col-sm-(1-12)

##### Eksempel:
```html
<div class="row">
	<div class="col-sm-8"></div>
	<div class="col-sm-4">
	   <div class="box"></div>
	</div>
</div>
```
##### Forms:
```html
<div class="form-horizontal">
	<div class="form-group">
		@Html.LabelFor(model => model.RoomName, new { @class = "control-label col-md-2" })
		<div class="col-md-10">
			@Html.TextBoxFor(model => model.RoomName, new { @class = "form-control" })
			@Html.ValidationMessageFor(model => model.RoomName)
		</div>
	</div>

	<div class="form-group">
		<div class="col-md-offset-2 col-md-10">
			<input type="submit" value="Save" class="btn btn-primary" />
		</div>
	</div>
</div>
```
