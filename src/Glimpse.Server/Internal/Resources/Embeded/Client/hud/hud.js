var script = document.querySelector('script[data-request-id]'),
link = document.createElement('a'),
requestId = script.dataset.requestId,
clientTemplate = script.dataset.clientTemplate;
// TODO: Remove .replace() as 'poor mans' uri template resolution.
link.setAttribute('href', clientTemplate.replace('{&requestId}', '&requestId=' + requestId));
link.setAttribute('target', '_blank');
link.text = 'Open Glimpse';
document.body.appendChild(link);