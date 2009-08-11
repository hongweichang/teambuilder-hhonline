(function() {
	// Load plugin specific language pack
	tinymce.PluginManager.requireLangPack('insertmedia');

	tinymce.create('tinymce.plugins.InsertMediaPlugin', {

		init : function(ed, url) {
		     // Register commands
			ed.addCommand('mceInsertMedia', function() {
    		if (tinyMCE_HHOnlineOptions)
    			tinyMCE_HHOnlineOptions.InsertMediaOpenFunction();	 
			});
			
            	// Register buttons
			ed.addButton('insertmedia', {
				title : 'insertmedia.desc',
				cmd : 'mceInsertMedia',
				image : url + '/img/insert_media.gif'
			});
		},

		getInfo : function() {
			return {
				longname : 'Insert Media',
				author : 'TigerYang',
				authorurl : 'http://hhonline.com/',
				infourl : 'http://hhonline.com/',
				version : "1.1"
			};
		}
	});

	// Register plugin
	tinymce.PluginManager.add('insertmedia', tinymce.plugins.InsertMediaPlugin);
})();

