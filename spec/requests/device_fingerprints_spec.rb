require 'rails_helper'

RSpec.describe "/device_fingerprints", type: :request do

  let!(:student) { Student.create!(full_name: "Jane Doe") }

  let(:valid_attributes) { { fingerprint_value: "unique_fingerprint_12345", last_seen_at: Time.current, student_id: student.id } }

  let(:invalid_attributes) { { fingerprint_value: nil, last_seen_at: "invalid_date", student_id: nil } }

  describe "GET /index" do
    it "renders a successful response" do
      DeviceFingerprint.create! valid_attributes
      get device_fingerprints_url
      expect(response).to be_successful
    end
  end

  describe "GET /show" do
    it "renders a successful response" do
      device_fingerprint = DeviceFingerprint.create! valid_attributes
      get device_fingerprint_url(device_fingerprint)
      expect(response).to be_successful
    end
  end

  describe "GET /new" do
    it "renders a successful response" do
      get new_device_fingerprint_url
      expect(response).to be_successful
    end
  end

  describe "GET /edit" do
    it "renders a successful response" do
      device_fingerprint = DeviceFingerprint.create! valid_attributes
      get edit_device_fingerprint_url(device_fingerprint)
      expect(response).to be_successful
    end
  end

  describe "POST /create" do
    context "with valid parameters" do
      it "creates a new DeviceFingerprint" do
        expect {
          post device_fingerprints_url, params: { device_fingerprint: valid_attributes }
        }.to change(DeviceFingerprint, :count).by(1)
      end

      it "redirects to the created device_fingerprint" do
        post device_fingerprints_url, params: { device_fingerprint: valid_attributes }
        expect(response).to redirect_to(device_fingerprint_url(DeviceFingerprint.last))
      end
    end

    context "with invalid parameters" do
      it "does not create a new DeviceFingerprint" do
        expect {
          post device_fingerprints_url, params: { device_fingerprint: invalid_attributes }
        }.to change(DeviceFingerprint, :count).by(0)
      end

      it "renders a response with 422 status (i.e. to display the 'new' template)" do
        post device_fingerprints_url, params: { device_fingerprint: invalid_attributes }
        expect(response).to have_http_status(:unprocessable_content)
      end
    end
  end

  describe "DELETE /destroy" do
    it "destroys the requested device_fingerprint" do
      device_fingerprint = DeviceFingerprint.create! valid_attributes
      expect {
        delete device_fingerprint_url(device_fingerprint)
      }.to change(DeviceFingerprint, :count).by(-1)
    end

    it "redirects to the device_fingerprints list" do
      device_fingerprint = DeviceFingerprint.create! valid_attributes
      delete device_fingerprint_url(device_fingerprint)
      expect(response).to redirect_to(device_fingerprints_url)
    end
  end
end
